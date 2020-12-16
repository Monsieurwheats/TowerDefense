using System.Collections;
using UnityEngine;

public class Canon : Tower
{

    protected override IEnumerator Shoot()
    {
        Minions[] minionsInRange = {};
        // TODO: Add condition (with round end)
        while (true)
        {
            yield return new WaitUntil( () =>
            {
                minionsInRange = MinionsInRange();
                return minionsInRange.Length != 0;
            });
            // TODO: Remove damage and only spawn bullet
            print("shoot on " +  minionsInRange[0]);
            minionsInRange[0].takeDmg(CurrLevel.damage);
            Debug.Log(CurrLevel.damage);
            Debug.Log(CurrLevel.secPerShot);
            
            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
    }
    
}


