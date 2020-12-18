using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class Game
{

    private static readonly string SavesPath = Application.persistentDataPath + "/saves/";
    private const string Ext = ".j9";

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

    public const int TileSize = 10;
    
    public static void StoreSave(Save save)
    {
        if (save == null) return;
        save.map = SceneManager.GetActiveScene().buildIndex;
        save.money = Player.Money;
        save.life = Player.Life;
        save.wave = WaveSpawner.WaveNumber;
        var towers = GameObject.FindGameObjectsWithTag("Tower");
        save.towerSaves = new Save.TowerSave[towers.Length];
        for (var i = 0; i < towers.Length; i++)
        {
            var go = towers[i];
            var tower = go.GetComponent<Tower>();
            var pos = go.transform.position;
            save.towerSaves[i] = new Save.TowerSave
            {
                level = tower.GetLevel(),
                prefab = tower.GetType().Name,
                x = pos.x,
                y = pos.y,
                z = pos.z
            };
        }
        var formatter = new BinaryFormatter();
        var path = SavesPath + save.id + Ext;
        var stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, save);
        stream.Close();
    }

    public static void LoadSave(Save save)
    {
        CurrSave = save;
        GameManager = null;
        Player = null;
        WaveSpawner = null;
        SceneManager.LoadScene(save.map);
        GameManagerLoaded.AddListener(() =>
        {
            // Place all towers
            foreach (var towerSave in save.towerSaves)
            {
                var go = GameManager.CreateTower(towerSave.prefab);
                go.transform.position = new Vector3(towerSave.x, towerSave.y, towerSave.z);
                var tower = go.GetComponent<Tower>();
                tower.SetLevel(towerSave.level);
                tower.CanBePlaced = true;
                tower.StartRound();
            }
            GameManagerLoaded = new UnityEvent();
        });
        PlayerLoaded.AddListener(() =>
        {
            Player.Money = save.money;
            Player.Life = save.life;
            PlayerLoaded = new UnityEvent();;
        });
        WaveSpawnerLoaded.AddListener(() =>
        {
            WaveSpawner.WaveNumber = save.wave;
            WaveSpawnerLoaded = new UnityEvent();
        });
    }
    
    private static Save GetSave(string file)
    {
        var path = SavesPath + file;
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found in " + path);
            return null;
            
        }
        var formatter = new BinaryFormatter();
        var stream = new FileStream(path, FileMode.Open);
        var save = (Save)formatter.Deserialize(stream);
        stream.Close();
        return save;
    }

    public static void DeleteSave(Save save)
    {
        if (save == null) return;
        var path = SavesPath + save.id + Ext;
        if (File.Exists(path)) File.Delete(path);
    }

    public static Save[] ListSaves()
    {
        var info = new DirectoryInfo(SavesPath);
        if (!info.Exists) info.Create();
        var files = info.GetFiles();
        var saves = files.Where(f => f.Exists && f.Extension == Ext)
            .Select(f => GetSave(f.Name)).ToArray();
        return saves;
    }

}
