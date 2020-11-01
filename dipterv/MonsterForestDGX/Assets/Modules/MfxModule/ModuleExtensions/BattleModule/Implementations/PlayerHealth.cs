
public class PlayerHealth : Health, ITarget
{
    protected override float GetBlockedDamage(float dmg)
    {
        if (damageBlock != null)
        {
            return damageBlock.GetCalculatedDamage(dmg);
        }
        else
        {
            return dmg;
        }
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if (currentHp < 0)
        {
            InitHealth();
        }
    }
}
