﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ArcherTower : Tower
{

    private int _shooter = 0;

    private int Shooter
    {
        get
        {
            _shooter += 1;
            _shooter %= CurrLevel.shootPoint.Length;
            return _shooter;
        }
    }

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
            // TODO: Take damage + spawn arrow
            print("shoot on " +  minionsInRange[0]);
            var shooter = CurrLevel.shootPoint[Shooter];
            var shooterPos = shooter.position;
            var targetDir = shooterPos - minionsInRange[0].transform.position;
            var angle = Vector3.SignedAngle(shooter.forward, targetDir, Vector3.up);
            shooter.RotateAround(shooterPos, Vector3.up, angle);
            minionsInRange[0].takeDmg(CurrLevel.damage); //ask oli where dmg is for tower
            
            yield return new WaitForSeconds(CurrLevel.secPerShot);
        }
    }
    
}
