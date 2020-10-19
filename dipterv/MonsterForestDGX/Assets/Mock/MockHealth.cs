
public class MockHealth : Health
{
    protected override float GetBlockedDamage(float dmg)
    {
        return dmg;
    }
}
