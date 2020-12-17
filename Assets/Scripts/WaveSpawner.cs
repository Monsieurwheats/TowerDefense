using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Waves[] waves;

    private int _eAlive;

    public int EAlive
    {
        get => _eAlive;
        set
        {
            _eAlive = value;
            Game.GameManager.IsPlaying = _eAlive > 0;
        }
    }

    public bool playing = false;
    private int WaveNumber { get; set; } = 0;

    public bool Playing => EAlive != 0;

    // Start is called before the first frame update
    void Start()
    {
        Game.WaveSpawner = this;
    }

    // Update is called once per frame



    IEnumerator  SpawnWave()
    {
        Debug.Log("Wavenumber: " + (WaveNumber));
        Waves wave = waves[WaveNumber];
        foreach(var e in wave.Enemies)
        {

           SpawnEnemy(e);
            yield return new WaitForSeconds(wave.rate);

        }
        yield return new WaitWhile(() => Playing);
        Game.Player.Money += 7;//5 bidous chaque round
        WaveNumber++;
        if(WaveNumber == waves.Length)
        {
            enabled = false;
            Game.GameManager.EndGame(true);
        }

        yield return null;
        
    }

    public void StartWave()
    {
        if (!Playing)
        {
            StartCoroutine(SpawnWave());
        }
        
    }

    void SpawnEnemy(Enemy enemy)
    {
        var x = Instantiate(enemy.minions, transform.position,transform.rotation);
        x.Level = enemy.level;
        EAlive++;
    }
    
    
    [System.Serializable]
    public class Waves
    {
        //public float count;
        public float rate;
        public Enemy[] Enemies = new Enemy[20];
        //public Minions[] e = new Minions[1];




    }

    [System.Serializable]
    public struct Enemy
    {
        public Minions minions;
        public int level;
    }

}
