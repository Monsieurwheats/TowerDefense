using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        var volume = PlayerPrefs.GetFloat("volume", 0.5f);
        AudioListener.volume = volume;
        volumeSlider.value = volume;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        var volume = volumeSlider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
    
    public void LoadMap(int map)
    {
        SceneManager.LoadScene(map);
    }
    
    public void OpenInfo()
    {
        Application.OpenURL("http://66.175.232.67/towerdefense/");
    }

}
