using System.Collections;
using UnityEngine;

public class SpearMan : Minions
{
    // Start is called before the first frame update
    public GameObject archer;
    public GameObject knight;
    int maxl;
    protected override void Start()
    {
        base.Start();
        maxl = base.level;
        agent.speed = agent.speed / 1.25f;
    }

    public override void setTexture()
    {
        if (level > -1)
        {
            m_renderer.material.mainTexture = textures[level];
        }
        else
        {
           
            StartCoroutine(SandD());
   
        }
    }
    protected override void Die()
    {
        Game.Player.Money += value;
        StartCoroutine(SandD());
    }
    IEnumerator SandD()
    {
        //int r = (Random.Range(0, 5) * 1000) % 5;
        //maybe make level random
        Game.WaveSpawner.EAlive += 2;
        var b = Instantiate(knight);
        b.transform.position = transform.position;
        b.transform.rotation = gameObject.transform.rotation;
        yield return new WaitForSeconds(0.001f);

        var a = Instantiate(archer);

        a.transform.position = transform.position;
        a.transform.rotation = gameObject.transform.rotation;
        a.GetComponent<Archer>().Level = maxl;
        b.GetComponent<Chevalier>().Level = maxl;
        base.Die();

        yield return null;
    }

}
