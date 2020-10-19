
public class PassiveFighterPart : FighterPart
{
    public Health health;

    protected override void FighterTakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }
}
