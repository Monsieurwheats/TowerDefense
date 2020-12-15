using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [NonSerialized] public Tower Tower;
    [NonSerialized] public Tower.Level Level;
    
    [SerializeField] private TMP_Text price;

    public void Start()
    {
        //TODO: Make better sprites
        GetComponent<Image>().sprite = Level.thumbnail;
        price.text = "$ " + Level.price;
    }
    
    public void Upgrade()
    {
        if (Game.Player.Money < Level.price) return;
        Tower.CurrLevel = Level;
        Game.Player.Money -= Level.price;
        Game.TowerUI.SelectedTower = Tower;
    }

}
