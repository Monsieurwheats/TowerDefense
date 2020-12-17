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
            
            Weapon.Create(CurrLevel.bullet, minionsInRange[0], Shooter, CurrLevel.damage);
            AudioSource.PlayClipAtPoint(shootingSound, this.transform.position);
            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
    }
    
}


