using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    public Player player = Game.Player;
    public WaveSpawner spawner;
    public static AudioSource UIsound;
    public static AudioSource GamePlaySound;
    public AudioClip wavestart;

    public void Start()
    {
        UIsound = GetComponents<AudioSource>()[0];
        GamePlaySound = GetComponents<AudioSource>()[1];
    }

    

    public void PlayerWave()
    {
        if(spawner.playing == false && spawner.enabled == true) // tempo cond maybe
        {
            UIsound.clip = wavestart;
            UIsound.Play();
            spawner.StartWave();
        }
  


    }
}
