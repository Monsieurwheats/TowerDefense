using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Transform scrollBoxContent = null;
    [SerializeField] private GameObject saveButtonPrefab = null;

    private Save[] _saves;

    private void Start()
    {
        var volume = PlayerPrefs.GetFloat("volume", 0.5f);
        AudioListener.volume = volume;
        volumeSlider.value = volume;
        
        // Prepare saves
        _saves = Game.ListSaves();
        foreach (var save in _saves)
        {
            var go = Instantiate(saveButtonPrefab, scrollBoxContent);
            var button = go.GetComponent<Button>();
            var texts = go.GetComponentsInChildren<TMP_Text>();
            button.onClick.AddListener(() => Game.LoadSave(save));
            texts[0].text = "Map " + save.map;
            texts[1].text = "HP: " + save.life + " | Money: " + save.money + " | Wave: " + save.wave;
        }
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
    
    public void StartNew(int map)
    {
        Game.CurrSave = new Save();
        SceneManager.LoadScene(map);
    }

    public void OpenInfo()
    {
        Application.OpenURL("http://66.175.232.67/towerdefense/");
    }

}
