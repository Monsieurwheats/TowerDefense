using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Waves[] waves;
    public static int EAlive = 0;

    private int waveNumber = 0;
    public bool playing = false;

    // Start is called before the first frame update
    void Start()
    {

         
    }

    // Update is called once per frame



    IEnumerator  SpawnWave()
    {
        Debug.Log("Wavenumber: " + (waveNumber));
        playing = true;
        Waves wave = waves[waveNumber];
        foreach(var e in wave.Enemies)
        {

           SpawnEnemy(e);
            yield return new WaitForSeconds(wave.rate);

        }
        while(EAlive > 0)
        {

            Debug.Log("Minions alive: " + EAlive);
            yield return new WaitForSeconds(1f);
        }
        waveNumber++;
        playing = false;
        if(waveNumber == waves.Length)
        {
            this.enabled = false;
        }
        yield return null;
        
    }

    public void StartWave()
    {
        if (playing == false)
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
