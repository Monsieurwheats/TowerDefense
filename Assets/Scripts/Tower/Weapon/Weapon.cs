using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    protected Minions Target;
    protected Vector3 Origin;

    public static Weapon Create(GameObject prefab, Minions target, Transform shootPoint)
    {
        var inst = Instantiate(prefab, shootPoint);
        var weapon = inst.GetComponent<Weapon>();
        weapon.Target = target;
        weapon.Origin = shootPoint.position;
        return weapon;
    }

    private void Start()
    {
        Shoot();
    }

    public abstract void Shoot();

}