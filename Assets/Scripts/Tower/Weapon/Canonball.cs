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

    private const float AnimationSpeed = 1f;
    private const float AnimationDuration = 1 / AnimationSpeed;


    public override void Shoot()
    {
        _realTarget = Target.transform.position;
        StartCoroutine(DoParabola());
    }

    private IEnumerator DoParabola()
    {
        var bullet = transform;
        while (_animationTime <= AnimationDuration)
        {
            _animationTime += Time.deltaTime;
            var newPos = MathParabola.Parabola(Origin, _realTarget, 10f, _animationTime * AnimationSpeed);
            bullet.position = newPos;
            bullet.LookAt(_realTarget);
            bullet.Rotate(Vector3.right, 90 * _animationTime / AnimationDuration);
            yield return null;
        }

        var inRange = Physics.OverlapSphere(transform.position, 5f);
        var minions = inRange.Select(c => c.GetComponent<Minions>()).Where(m => m != null).ToArray();
        foreach (var minion in minions)
        {
            if (minion) minion.takeDmg(Damage);
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
