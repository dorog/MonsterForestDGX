
public class InteractDamageBlock : DamageBlock
{
    private bool inBlock = false;

    public override void StartBlocking()
    {
        inBlock = true;
    }

    public override float GetCalculatedDamage(float damage)
    {
        if (inBlock)
        {
            inBlock = false;
            BlockDowned();
            return CalculateDamage(blockValue, damage);
        }

        return damage;
    }
}
