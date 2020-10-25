﻿using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;
    public Fighter fighter;

    public ExtraFighterPart[] extraParts;

    public DamageBlock damageBlock;

    public HealthShowerUI healthShowerUI;

    [Header("Only for healing (optional)")]
    public ParticleSystem healEffect;

    public virtual void InitHealth()
    {
        currentHp = maxHp;
        foreach (var extraPart in extraParts)
        {
            extraPart.Appear();
        }

        if (healthShowerUI != null)
        {
            healthShowerUI.ShowHealthData(currentHp, maxHp);
        }
    }

    public virtual void TakeDamage(float dmg)
    {
        float realDmg = GetBlockedDamage(dmg);

        currentHp -= realDmg;

        if (currentHp <= 0)
        {
            DisableHealth();
            fighter.Die();
        }

        if (healthShowerUI != null)
        {
            healthShowerUI.ShowHealthData(currentHp, maxHp);
        }
    }

    protected virtual float GetBlockedDamage(float dmg)
    {
        return damageBlock.GetCalculatedDamage(dmg);
    }

    public virtual void SetDamageBlock()
    {
        damageBlock.StartBlocking();
    }

    public bool IsFull()
    {
        return currentHp == maxHp;
    }

    public void Heal(float amount)
    {
        if (maxHp - currentHp >= amount)
        {
            currentHp += amount;
        }
        else
        {
            currentHp = maxHp;
        }

        if(healEffect != null)
        {
            healEffect.Play();
        }
    }

    public virtual void DisableHealth()
    {
        foreach (var extraPart in extraParts)
        {
            extraPart.Disable();
        }
    }
}
