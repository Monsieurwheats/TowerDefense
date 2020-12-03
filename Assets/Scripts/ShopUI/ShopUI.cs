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
        _towerToPlace = inst;
        Drag();
    }

    public void StopDrag()
    {
        _towerToPlace.Place();
        _towerToPlace = null;
    }

    public void Drag()
    {
        var ray = Game.Cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo, float.MaxValue, LayerMask.GetMask("Ground"))) return; // Return if no hit
        if (!Game.Map.Tiles.Contains(hitInfo.collider.transform)) return; // Return if not floor
        _towerToPlace.TryPlace(hitInfo);
    }

}
