using System;
using UnityEngine;

public class AttackableConnector : MonoBehaviour, IAttackable
{
    public Health health;
    public ParticleSystem petAttackEffect;
    public Fighter fighter;

    public BattleManager battleManager;
    public GameEvents gameEvents;

    public void TakeDamageFromPet(float amount)
    {
        health.TakeDamage(amount, ElementType.TrueDamage);
        if (petAttackEffect != null)
        {
            petAttackEffect.Play();
        }
    }

    public void SubscribeToAttackEvents(Action activate, Action deactivate)
    {
        if(gameEvents.GetBlueFighter() == fighter)
        {
            battleManager.BlueFighterTurnStartDelegateEvent += activate;
            battleManager.BlueFighterTurnEndDelegateEvent += deactivate;
        }
        else if (gameEvents.GetRedFighter() == fighter)
        {
            battleManager.RedFighterTurnStartDelegateEvent += activate;
            battleManager.RedFighterTurnEndDelegateEvent += deactivate;
        }
    }

    public void UnsubscribeFromAttackEvents(Action activate, Action deactivate)
    {
        if (gameEvents.GetBlueFighter() == fighter)
        {
            battleManager.BlueFighterTurnStartDelegateEvent -= activate;
            battleManager.BlueFighterTurnEndDelegateEvent -= deactivate;
        }
        else if (gameEvents.GetRedFighter() == fighter)
        {
            battleManager.RedFighterTurnStartDelegateEvent -= activate;
            battleManager.RedFighterTurnEndDelegateEvent -= deactivate;
        }
    }
}
