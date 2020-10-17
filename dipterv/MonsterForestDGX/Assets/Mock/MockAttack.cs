
public class MockAttack : IAttack
{
    public int AttackedCounter = 0;

    public override float Attack()
    {
        AttackedCounter++;

        return 0;
    }
}
