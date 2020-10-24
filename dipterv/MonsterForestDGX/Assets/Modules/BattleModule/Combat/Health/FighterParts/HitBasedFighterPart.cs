
public class HitBasedFighterPart : FighterPart
{
    public HitType monsterHitType;
    public AnimatedHealth health;

    protected override void FighterTakeDamage(float damage)
    {
        if (monsterHitType == HitType.Body)
        {
            health.TakeDamageBasedOnHit(damage, false);
        }
        else
        {
            health.TakeDamageBasedOnHit(damage, true);
        }
    }

    public enum HitType
    {
        Body, Head
    }
}
