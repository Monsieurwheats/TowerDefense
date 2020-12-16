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

}
