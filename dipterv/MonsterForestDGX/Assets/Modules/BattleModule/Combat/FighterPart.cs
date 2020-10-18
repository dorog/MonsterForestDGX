using UnityEngine;

public class FighterPart : MonoBehaviour, ITarget
{
    public Health health;
    public HitType monsterHitType;
    [Range(0, 200)]
    public float dmgPercent = 100;

    public void TakeDamage(float dmg, ElementType magicType)
    {
        float damage = dmg * dmgPercent / 100;
        if(monsterHitType == HitType.Body)
        {
            health.TakeDamageBasedOnHit(damage, magicType, false);
        }
        else
        {
            health.TakeDamageBasedOnHit(damage, magicType, true);
        }
    }

    public void TakeDamage(float dmg, ElementType elementType, Health attackerHealth)
    {
        if(attackerHealth == health)
        {
            return;
        }
        TakeDamage(dmg, elementType);
    }
}

public enum HitType
{
    Body, Head
}
