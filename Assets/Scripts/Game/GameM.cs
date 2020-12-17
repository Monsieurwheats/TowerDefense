using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameM : MonoBehaviour
{
    public AudioSource UIsound;
    public AudioSource GamePlaySound;
    public AudioClip wavestart;

    [SerializeField] private Image fastButton;

    private bool _isFast;

    private bool IsFast
    {
        get => _isFast;
        set
        {
            _isFast = value;
            fastButton.color = _isFast ? Color.yellow : Color.gray;
        }
        
    }

    public void Start()
    {
        Game.GameManager = this;
        IsFast = false;
    }

    

    public void PlayerWave()
    {
        if (Game.WaveSpawner.playing || !Game.WaveSpawner.enabled) return;
        UIsound.clip = wavestart;
        UIsound.Play();
        Game.WaveSpawner.StartWave();
    }

    public void Fast()
    {
        IsFast = !IsFast;
        Time.timeScale = IsFast ? 2 : 1;
    }


    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
