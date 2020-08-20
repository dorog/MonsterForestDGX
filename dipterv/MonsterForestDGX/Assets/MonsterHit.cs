using UnityEngine;

public class MonsterHit : MonoBehaviour, ITarget
{
    public Health health;
    public MonsterHitType monsterHitType;
    [Range(0, 200)]
    public float dmgPercent = 100;

    public void TakeDamage(float dmg, ElementType magicType)
    {
        float damage = dmg * dmgPercent / 100;
        if(monsterHitType == MonsterHitType.Body)
        {
            health.TakeDamageBasedOnHit(damage, magicType, false);
        }
        else
        {
            health.TakeDamageBasedOnHit(damage, magicType, true);
        }
    }
}

public enum MonsterHitType
{
    Body, Head
}
