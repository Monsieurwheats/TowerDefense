using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    protected Minions Target;
    protected Vector3 Origin;
    protected int Damage;

    public static Weapon Create(GameObject prefab, Minions target, Transform shootPoint, int damage)
    {
        var inst = Instantiate(prefab, shootPoint);
        var weapon = inst.GetComponent<Weapon>();
        weapon.Target = target;
        weapon.Origin = shootPoint.position;
        weapon.Damage = damage;
        return weapon;
    }

    private void Start()
    {
        Shoot();
    }

    public abstract void Shoot();

}