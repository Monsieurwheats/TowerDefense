using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Minions
{

    protected override void Start()
    {
        base.Start();
        StartCoroutine(checkMinions());
    }
    WaitForSeconds refresh = new WaitForSeconds(4f);
    float radius = 3f;
    Vector3 center;
    IEnumerator checkMinions()
    {

        
        //chek condition should be while wave is playing
        while (true)
        {
            yield return refresh;
            center = transform.position;
            Collider[] colliders = Physics.OverlapSphere(center, radius);
            foreach (var hit in colliders)
            {
                var h = hit.GetComponent<Minions>();
                if (h!= null)
                {
                    //heal minion
                    if(h.Level < 4) { h.Level++; h.setTexture(); }
                    
                    
                    

                    
                }
            }
        }
    }
}
