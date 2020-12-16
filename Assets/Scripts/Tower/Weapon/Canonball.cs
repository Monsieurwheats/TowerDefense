using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Canonball : Weapon
{

    [SerializeField] private GameObject explosion = null;
    
    private float _animationTime;
    private Vector3 _realTarget;


    public override void Shoot()
    {
        _realTarget = Target.transform.position;
        StartCoroutine(DoParabola());
    }

    private IEnumerator DoParabola()
    {
        var bullet = transform;
        while (transform.position.y > 0)
        {
            // TODO: Add rotation
            _animationTime += Time.deltaTime;
            _animationTime %= 5f;
            var newPos = MathParabola.Parabola(Origin, _realTarget, 5f, _animationTime);
            bullet.position = newPos;
            bullet.LookAt(_realTarget);
            yield return null;
        }

        var inRange = Physics.OverlapSphere(transform.position, 5f);
        var minions = inRange.Select(c => c.GetComponent<Minions>()).Where(m => m != null).ToArray();
        foreach (var minion in minions)
        {
            minion.takeDmg(Damage);
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
