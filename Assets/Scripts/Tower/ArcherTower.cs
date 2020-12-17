using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ArcherTower : Tower
{

    protected override IEnumerator Shoot()
    {
        Minions[] minionsInRange = {};
        // TODO: Add condition (with round end)
        while (true)
        {
            yield return new WaitUntil(() =>
            {
                minionsInRange = MinionsInRange();
                return minionsInRange.Length != 0;
            });
            var shooterTransform = Shooter;
            var shooterPos = shooterTransform.position;
            var targetDir = shooterPos - minionsInRange[0].transform.position;
            var angle = Vector3.SignedAngle(shooterTransform.forward, targetDir, Vector3.up);
            shooterTransform.RotateAround(shooterPos, Vector3.up, angle); // Make archer look at enemy
            Weapon.Create(CurrLevel.bullet, minionsInRange[0], shooterTransform, CurrLevel.damage);
            AudioSource.PlayClipAtPoint(shootingSound, transform.position);
            
            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
    }
    
}
