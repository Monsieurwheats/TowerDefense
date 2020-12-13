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
    
    private int _money = 50;
    private int _life = 100;

    [SerializeField] private TMP_Text moneyText = null;
    [SerializeField] private TMP_Text lifeText = null;

    private void Start()
    {
        Game.Player = this;
        Refresh();
    }

    private void Refresh()
    {
        moneyText.text = _money.ToString();
        lifeText.text = _life.ToString();
    }
    
}
