
using UnityEngine;

public class PassiveFighterPart : FighterPart
{
    public Health health;

    protected override void FighterTakeDamage(float damage)
    {
        Debug.Log(health.gameObject.name);
        health.TakeDamage(damage);
    }
}
