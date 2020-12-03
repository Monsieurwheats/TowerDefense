public class Archer : Minions
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        base.agent.speed *= 2f;
    }

}
