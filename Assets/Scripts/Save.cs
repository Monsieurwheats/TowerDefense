using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[Serializable]
public class Save
{
   
    private static readonly string SavesPath = Application.persistentDataPath + "/saves/";
    private const string Ext = ".j9";
    
    public string id;
    public int map;
    public int money;
    public int life;
    public int wave;
    public TowerSave[] towerSaves;
    
    public Save()
    {
        id = Guid.NewGuid().ToString();
    }
    
    public void Store()
    {
        map = SceneManager.GetActiveScene().buildIndex;
        money = Game.Player.Money;
        life = Game.Player.Life;
        wave = Game.WaveSpawner.WaveNumber;
        var towers = GameObject.FindGameObjectsWithTag("Tower");
        towerSaves = new Save.TowerSave[towers.Length];
        for (var i = 0; i < towers.Length; i++)
        {
            var go = towers[i];
            var tower = go.GetComponent<Tower>();
            var pos = go.transform.position;
            towerSaves[i] = new Save.TowerSave
            {
                level = tower.GetLevel(),
                prefab = tower.GetType().Name,
                x = pos.x,
                y = pos.y,
                z = pos.z
            };
        }
        var formatter = new BinaryFormatter();
        var path = SavesPath + id + Ext;
        var stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, this);
        stream.Close();
    }
    
    public void Load()
    {
        Game.CurrSave = this;
        SceneManager.LoadScene(map);
        Game.GameManagerLoaded.AddListener(() =>
        {
            // Place all towers
            foreach (var towerSave in towerSaves)
            {
                var go = Game.GameManager.CreateTower(towerSave.prefab);
                go.transform.position = new Vector3(towerSave.x, towerSave.y, towerSave.z);
                var tower = go.GetComponent<Tower>();
                tower.CanBePlaced = true;
                tower.SetLevel(towerSave.level);
                tower.StartRound();
            }
            Game.GameManagerLoaded = new UnityEvent();
        });
        Game.PlayerLoaded.AddListener(() =>
        {
            Game.Player.Money = money;
            Game.Player.Life = life;
            Game.PlayerLoaded = new UnityEvent();;
        });
        Game.WaveSpawnerLoaded.AddListener(() =>
        {
            Game.WaveSpawner.WaveNumber = wave;
            Game.WaveSpawnerLoaded = new UnityEvent();
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
    
    public static Save[] ListSaves()
    {
        var info = new DirectoryInfo(SavesPath);
        if (!info.Exists) info.Create();
        var files = info.GetFiles();
        var saves = files.Where(f => f.Exists && f.Extension == Ext)
            .Select(f => GetSave(f.Name)).ToArray();
        return saves;
    }
    
    public void Delete()
    {
        var path = SavesPath + id + Ext;
        if (File.Exists(path)) File.Delete(path);
    }
    
    [Serializable]
    public struct TowerSave
    {
        public string level;
        public string prefab;
        public float x;
        public float y;
        public float z;
    }
}