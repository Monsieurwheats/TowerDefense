using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    public override void Shoot()
    {
        if (Target)
        {
            var line = GetComponent<LineRenderer>();
            line.SetPositions(new [] {Origin, Target.transform.position});
            Target.takeDmg(Damage);
        }
        StartCoroutine(Remove());
    }

    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
