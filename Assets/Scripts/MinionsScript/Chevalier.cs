using UnityEngine;

public class Chevalier : Minions
{
    // Start is called before the first frame update
    float hp;

    protected override void Start()
    {

        base.Start();
        hp = Level * 2;

    }
    public override void takeDmg(int dmg)
    {
        hp -= dmg;
      //  Debug.Log((int)(hp / 2 + 0.5));
        Level = hp > 0 ? (int)(hp / 2 + 0.5) : (int)(hp / 2);
        base.setTexture();

    }


}
