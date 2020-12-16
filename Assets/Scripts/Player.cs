using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Money
    {
        set { _money = value; Refresh(); }
        get => _money;
    }

    public int Life
    {
        set { _life = value; Refresh(); }
        get => _life;
    }
    
    private int _money = 30;
    private int _life = 10;

    [SerializeField] private TMP_Text moneyText = null;
    [SerializeField] private TMP_Text lifeText = null;

    private void Start()
    {
        Game.Player = this;
        Refresh();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            Money += 1000;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Life += 1000;
        }
        #endif
    }

    private void Refresh()
    {
        moneyText.text = _money.ToString();
        lifeText.text = _life.ToString();
    }
    
}
