using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    public AudioSource UIsound;
    public AudioSource GamePlaySound;
    public AudioClip wavestart;

    public void Start()
    {
        Game.GameManager = this;
    }

    

    public void PlayerWave()
    {
        if(Game.WaveSpawner.playing == false && Game.WaveSpawner.enabled == true) // tempo cond maybe
        {
            UIsound.clip = wavestart;
            UIsound.Play();
            Game.WaveSpawner.StartWave();
        }
  


    }
}
