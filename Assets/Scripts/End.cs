using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    WaitForSeconds refresh = new WaitForSeconds(0.2f);
    float radius = 4f;
    Vector3 center;
    void Start()
    {
        center = transform.position;
        StartCoroutine(checkMinions());
    }

    IEnumerator checkMinions()
    {
        //chek condition should be while wave is playing
        while (true)
        {
            yield return refresh;
            Collider[] colliders = Physics.OverlapSphere(center, radius);
            foreach(var hit in colliders)
            {
                if(hit.GetComponent<Minions>() != null)
                {
                    //+1 since first level is level 0;
                    int dmg = hit.GetComponent<Minions>().damage * (hit.GetComponent<Minions>().Level + 1);
                    Game.Player.Life -= dmg;
                    Game.WaveSpawner.EAlive--;
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4);
    }

}
