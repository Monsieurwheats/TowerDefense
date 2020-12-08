using UnityEngine;

public class Chevalier : Minions
{
    // Start is called before the first frame update
    float hp = 8;

    protected override void Start()
    {
        base.Start();


    }
    public override void takeDmg(int dmg)
    {
        hp -= dmg;
        //Debug.Log((int)(hp / 2 + 0.5));
        Level = hp > 0 ? (int)(hp / 2 + 0.5) : (int)(hp / 2);
        base.setTexture();

    }


}
