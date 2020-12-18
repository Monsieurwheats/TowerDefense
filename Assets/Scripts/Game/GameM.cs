using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameM : MonoBehaviour
{
    public AudioSource UIsound;
    public AudioSource GamePlaySound;
    public AudioClip wavestart;

    [SerializeField] private Image fastButton = null;
    [SerializeField] private Image playButton = null;
    [SerializeField] private GameObject endScreen = null;
    [SerializeField] private TMP_Text endText = null;

    [SerializeField] private GameObject ArcherTowerPrefab;
    [SerializeField] private GameObject CanonPrefab;
    [SerializeField] private GameObject MageTowerPrefab;
    
    private bool _isFast;

    private bool IsFast
    {
        get => _isFast;
        set
        {
            _isFast = value;
            PlayerPrefs.SetInt("IsFast", _isFast? 1 : 0);
            fastButton.color = _isFast ? Color.yellow : Color.gray;
            Time.timeScale = IsFast ? 2 : 1;
        }
        
    }

    public bool IsPlaying
    {
        set => playButton.color = value ? Color.gray : Color.green;
    }
    

    public void Start()
    {
        Game.GameManager = this;
        IsFast = PlayerPrefs.GetInt("IsFast", 0) == 1;
        IsPlaying = false;
        endScreen.SetActive(false);
        Game.GameManagerLoaded.Invoke();
    }

    public GameObject CreateTower(string type)
    {
        GameObject prefab;
        switch (type)
        {
            case "ArcherTower":
                prefab = ArcherTowerPrefab;
                break;
            case "Canon":
                prefab = CanonPrefab;
                break;
            case "MageTower":
                prefab= MageTowerPrefab;
                break;
            default:
                return null;
        }

        var go = Instantiate(prefab);
        return go;
    }

    public void PlayerWave()
    {
        if (Game.WaveSpawner.Playing || !Game.WaveSpawner.enabled) return;
        UIsound.clip = wavestart;
        UIsound.Play();
        Game.WaveSpawner.StartWave();
    }

    public void Fast()
    {
        IsFast = !IsFast;
    }
    
    public void EndGame(bool hasWon)
    {
        var word = hasWon ? "You Win!" : "You Lose";
        endText.text = word;
        endScreen.SetActive(true);
        Game.DeleteSave(Game.CurrSave);
        Game.CurrSave = null;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
