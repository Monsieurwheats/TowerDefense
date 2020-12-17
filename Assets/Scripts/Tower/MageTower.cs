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
            Weapon.Create(CurrLevel.bullet, minionsInRange[0], Shooter, CurrLevel.damage);
            AudioSource.PlayClipAtPoint(shootingSound, transform.position);

            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
        yield return null;
    }
}
