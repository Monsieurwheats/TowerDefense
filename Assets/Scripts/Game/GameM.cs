using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    public Player player = Game.Player;
    public WaveSpawner spawner;
    public static AudioSource UIsound;

    public void Start()
    {
        UIsound = GetComponents<AudioSource>()[0];
    }


    public void PlayerWave()
    {
        if(spawner.playing == false && spawner.enabled == true) // tempo cond maybe
        {
            spawner.StartWave();
        }
  


    }
}
