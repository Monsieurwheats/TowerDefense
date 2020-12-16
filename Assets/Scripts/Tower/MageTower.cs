using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
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
            Weapon.Create(CurrLevel.bullet, minionsInRange[0], Shooter);
            minionsInRange[0].takeDmg(CurrLevel.damage);
            
            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
        yield return null;
    }
}
