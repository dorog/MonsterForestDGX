
public class FixDamageModifier : DamageModifier
{
    public int fixDamage = 1;

    public override float CalculateDamage(float damage)
    {
        return fixDamage;
    }
}
