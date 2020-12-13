using System.Collections;
using System.Linq;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    public bool IsPlacingTower => _towerToPlace != null;

    private IEnumerator _helpRoutine;
    private Tower _towerToPlace;

    private void Start()
    {
        Game.ShopUI = this;
    }

    public void OpenHelp(string message)
    {
        _helpRoutine = Help(message);
        StartCoroutine(_helpRoutine);
    }

    public void CloseHelp()
    {
        if (_helpRoutine != null)
            StopCoroutine(_helpRoutine);
    }

    private IEnumerator Help(string message)
    {
        yield return new WaitForSeconds(1);
        print(message);
    }

    public void StartDrag(GameObject tower)
    {
        var inst = Instantiate(tower).GetComponent<Tower>();
        if (Game.Player.Money >= inst.Price) 
        {
            _towerToPlace = inst; 
            Drag();
        }
        else Destroy(inst);
    }

    public void StopDrag()
    {
        if (_towerToPlace == null) return;
        if (Game.Player.Money >= _towerToPlace.Price)
        {
            var wasPlaced = _towerToPlace.Place();
            if (wasPlaced) Game.Player.Money -= _towerToPlace.Price;
        }
        _towerToPlace = null;
    }

    public void Drag()
    {
        if (_towerToPlace == null) return;
        var ray = Game.Cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, LayerMask.GetMask("Ground"))) return; // Return if no hit
        if (!Game.Map.Tiles.Contains(hitInfo.collider.transform)) return; // Return if not floor
        _towerToPlace.TryPlace(hitInfo);
    }

}
