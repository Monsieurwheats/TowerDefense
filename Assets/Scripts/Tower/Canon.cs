using System.Collections;

public class Canon : Tower
{

    protected override IEnumerator Shoot()
    {
        print("piew");
        yield return null;
    }
}


