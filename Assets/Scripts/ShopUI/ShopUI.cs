using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    public bool IsPlacingTower => _towerToPlace != null;

    private IEnumerator _helpRoutine;
    private GameObject _towerToPlace;
    
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
        StopCoroutine(_helpRoutine);
    }

    private IEnumerator Help(string message)
    {
        yield return new WaitForSeconds(1);
        print(message);
    }

    public void StartDrag(GameObject tower)
    {
        _towerToPlace = tower;
    }

    public void StopDrag()
    {
        _towerToPlace = null;
        Game.MapUI.ClearCanvas();
    }
    
}
