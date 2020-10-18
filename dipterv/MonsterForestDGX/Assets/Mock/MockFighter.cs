
public class MockFighter : AiFighter
{
    public override void Disable(){}

    public override EnemyType IsMonster()
    {
        return EnemyType.Dummy;
    }

    protected override void Appear(){}

    protected override void Disappear(){}

    protected override void React(){}

    protected override void ResetMonster() {}
}
