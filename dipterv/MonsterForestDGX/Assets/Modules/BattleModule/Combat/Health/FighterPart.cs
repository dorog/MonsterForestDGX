using UnityEngine;

public class FighterPart : MonoBehaviour, ITarget
{
    public Health health;
    public HitType monsterHitType;
    [Range(0, 200)]
    public float dmgPercent = 100;

    public void TakeDamage(float dmg)
    {
        float damage = dmg * dmgPercent / 100;
        if(monsterHitType == HitType.Body)
        {
            health.TakeDamageBasedOnHit(damage, false);
        }
        else
        {
            health.TakeDamageBasedOnHit(damage, true);
        }
    }

    public void TakeDamage(float dmg, Health attackerHealth)
    {
        if(attackerHealth == health)
        {
            return;
        }
        TakeDamage(dmg);
    }
}

public enum HitType
{
    Body, Head
}
