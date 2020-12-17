using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{

    private float _animationTime;
    private const float AnimationSpeed = 5f;
    private const float AnimationDuration = 1/AnimationSpeed;
    
    public override void Shoot()
    {
        StartCoroutine(Travel());
    }

    private IEnumerator Travel()
    {
        var arrowTransform = transform;
        var targetTransform = Target.transform;
        while (_animationTime <= AnimationDuration && Target)
        {
            _animationTime += Time.deltaTime;
            var targetPos = targetTransform.position;
            var newPos = MathParabola.Parabola(Origin, targetPos, 0f, _animationTime * AnimationSpeed);
            arrowTransform.position = newPos;
            arrowTransform.LookAt(targetPos);
            arrowTransform.Rotate(Vector3.right, -90);
            yield return null;
        }
        if (Target)
        {
            Target.takeDmg(Damage);
        }
        Destroy(gameObject);
    }
}
