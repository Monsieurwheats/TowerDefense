using System.Collections;
using UnityEngine;

public class Canon : Tower
{

    protected override IEnumerator Shoot()
    {
        Minions[] minionsInRange = {};
        while (true)
        {
            yield return new WaitUntil( () =>
            {
                minionsInRange = MinionsInRange();
                return minionsInRange.Length != 0;
            });
            print("shoot on " +  minionsInRange[0]);
            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
        yield return null;
    }
    
}


