﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Text hp;
    public GameObject block;

    public TimeDamageBlock timeDamageBlock;

    public ParticleSystem healEffect;

    public override void SetUpHealth()
    {
        base.SetUpHealth();
        
        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }

    public void BlockDown()
    {
        block.SetActive(false);
    }

    public void Heal(float amount)
    {
        if(maxHp - currentHp >= amount)
        {
            currentHp += amount;
        }
        else
        {
            currentHp = maxHp;
        }

        SetUpHealth();

        healEffect.Play();
    }

    protected override float GetBlockedDamage(float dmg)
    {
        if (timeDamageBlock != null)
        {
            return timeDamageBlock.GetCalculatedDamage(dmg);
        }
        else
        {
            return dmg;
        }
    }

    public override void SetDamageBlock()
    {
        if (timeDamageBlock != null)
        {
            timeDamageBlock.StartBlock();
        }
    }

    public override void TakeDamageBasedOnHit(float dmg, ElementType magicType, bool isHeadshot)
    {
        
    }
}
