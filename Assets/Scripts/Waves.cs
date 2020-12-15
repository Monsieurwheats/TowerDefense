using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    //public float count;
    public float rate;
    public Enemy[] Enemies = new Enemy[20];
    //public Minions[] e = new Minions[1];




}

[System.Serializable]
public struct Enemy
{
    public Minions minions;
    public int level;
}
