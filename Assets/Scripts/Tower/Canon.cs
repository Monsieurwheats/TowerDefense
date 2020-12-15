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
            print("shoot on " +  minionsInRange[0]);
            minionsInRange[0].takeDmg(1); //ask oli where dmg is for tower
            
            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
        yield return null;
    }
    
}


