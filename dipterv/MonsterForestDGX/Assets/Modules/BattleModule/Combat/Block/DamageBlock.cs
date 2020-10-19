using System;
using UnityEngine;

public abstract class DamageBlock : MonoBehaviour
{
    public float blockValue = 10;

    private event Action BlockDown;

    public abstract float GetCalculatedDamage(float damage);

    protected float CalculateDamage(float value, float dmg)
    {
        float blockedDamage = dmg;

        if (value >= blockedDamage)
        {
            blockedDamage = 0;
        }
        else
        {
            blockedDamage -= value;
        }

        return blockedDamage;
    }

    public void SubscribeToBlockDown(Action method)
    {
        BlockDown += method;
    }

    public void UnsubscribeToBlockDown(Action method)
    {
        BlockDown -= method;
    }

    protected void BlockDowned()
    {
        BlockDown?.Invoke();
    }

    public abstract void StartBlocking();
}
