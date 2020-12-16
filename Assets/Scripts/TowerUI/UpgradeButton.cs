using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{ 
    private Tower _tower;
    private Tower.Level _level;

    public void Start()
    {
        //TODO: Make better sprites
        GetComponent<Image>().sprite = _level.thumbnail;
        GetComponentInChildren<TMP_Text>().text = "$ " + _level.price;
    }

    public static UpgradeButton Create(GameObject prefab, Transform uiTransform, Tower.Level level, Tower tower)
    {
        var upgrade = Instantiate(prefab, uiTransform).GetComponent<UpgradeButton>();
        upgrade._level = level;
        upgrade._tower = tower;
        return upgrade;
    }
    
    public void Upgrade()
    {
        if (Game.Player.Money < _level.price) return;
        _tower.CurrLevel = _level;
        Game.Player.Money -= _level.price;
        Game.TowerUI.SelectedTower = _tower;
    }

}
