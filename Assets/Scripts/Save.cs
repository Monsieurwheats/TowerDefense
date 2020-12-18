using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[Serializable]
public class Save
{
   
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