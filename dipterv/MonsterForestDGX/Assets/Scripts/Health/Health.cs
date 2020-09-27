using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour, IAttackable, IHealable
{
    public float maxHp = 100f;
    public float currentHp;
    public Fighter fighter;
    //public Slider hpSlider;
    public Image hpImage;
    public Color lowColor;
    public Color fullColor;
    public Resistant resistant;

    [Header("Only for healing (optional)")]
    public ParticleSystem healEffect;
    public ParticleSystem petAttackEffect;

    public void Start()
    {
        currentHp = maxHp;
    }

    public virtual void SetUpHealth()
    {
        //hpSlider.value = currentHp / maxHp;
        //hpImage.color = Color.Lerp(lowColor, fullColor, currentHp / maxHp);
    }

    public virtual void TakeDamage(float dmg, ElementType magicType)
    {
        float realDmg = resistant.CalculateDmg(dmg, magicType);

        realDmg = GetBlockedDamage(realDmg);

        currentHp -= realDmg;

        SetUpHealth();

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
        SetUpHealth();
    }

    public void TakeDamageFromPet(float amount)
    {
        TakeDamage(amount, ElementType.TrueDamage);
        if(petAttackEffect != null)
        {
            petAttackEffect.Play();
        }
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

        SetUpHealth();

        if(healEffect != null)
        {
            healEffect.Play();
        }
    }

    public void SubscribeToAttackEvents(Action activate, Action deactivate)
    {
        BattleManager battleManager = BattleManager.GetInstance();
        battleManager.PlayerTurnStartDelegateEvent += activate;
        battleManager.PlayerTurnEndDelegateEvent += deactivate;
    }

    public void UnsubscribeFromAttackEvents(Action activate, Action deactivate)
    {
        BattleManager battleManager = BattleManager.GetInstance();
        battleManager.PlayerTurnStartDelegateEvent -= activate;
        battleManager.PlayerTurnEndDelegateEvent -= deactivate;
    }

    public void SubscribeToHealEvents(Action activate, Action deactivate)
    {
        GameEvents gameEvents = GameEvents.GetInstance();
        activate?.Invoke();
        gameEvents.BattleEndDelegateEvent += deactivate;
    }

    public void UnsubscribeFromHealEvents(Action activate, Action deactivate)
    {
        GameEvents gameEvents = GameEvents.GetInstance();
        gameEvents.BattleEndDelegateEvent -= deactivate;
    }
}
