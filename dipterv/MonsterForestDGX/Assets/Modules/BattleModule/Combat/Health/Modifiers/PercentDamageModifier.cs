using UnityEngine;

public class PercentDamageModifier : DamageModifier
{
    [Range(0, 200)]
    public float dmgPercent = 100;

    public override float CalculateDamage(float damage)
    {
        return damage * (dmgPercent / 100);
    }
}
