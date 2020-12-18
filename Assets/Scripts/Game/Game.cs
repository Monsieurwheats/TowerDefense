using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class Game
{

    public static UnityEvent GameManagerLoaded = new UnityEvent();
    public static UnityEvent PlayerLoaded = new UnityEvent();
    public static UnityEvent WaveSpawnerLoaded = new UnityEvent();
    
    public static Map Map = null;

    public static ShopUI ShopUI = null;
    
    public static TowerUI TowerUI = null;

    public static UnityEngine.Camera Cam = null;

    public static Player Player = null;

    public static WaveSpawner WaveSpawner = null;

    public static GameM GameManager;

    public static Save CurrSave;

    public static MusicPlayer MusicPlayer = null;

}
