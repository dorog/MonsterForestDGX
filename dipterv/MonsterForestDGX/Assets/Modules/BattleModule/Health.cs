﻿using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;
    public Fighter fighter;
    public Resistant resistant;

    [Header("Only for healing (optional)")]
    public ParticleSystem healEffect;

    public void Start()
    {
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float dmg, ElementType magicType)
    {
        float realDmg = resistant.CalculateDmg(dmg, magicType);

        realDmg = GetBlockedDamage(realDmg);

        currentHp -= realDmg;

        if (currentHp <= 0)
        {
            fighter.Die();
        }
    }

    protected abstract float GetBlockedDamage(float dmg);

    public abstract void SetDamageBlock();

    public abstract void TakeDamageBasedOnHit(float dmg, ElementType magicType, bool isHeadshot);

    public virtual void ResetHealth()
    {
        currentHp = maxHp;
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
}
