using System;
using UnityEngine;

public class AttackableHandler : MonoBehaviour, IAttackable
{
    public Health health;
    public Fighter fighter;

    [Header ("Optional")]
    public ParticleSystem petAttackEffect;

    public void TakeDamageFromPet(float amount)
    {
        health.TakeDamage(amount);
        if (petAttackEffect != null)
        {
            petAttackEffect.Play();
        }
    }

    public void SubscribeToAttackEvents(Action activate, Action deactivate)
    {
        fighter.SubscribeToStartTurn(activate);
        fighter.SubscribeToEndTurn(deactivate);
    }

    public void UnsubscribeFromAttackEvents(Action activate, Action deactivate)
    {
        fighter.UnsubscribeToStartTurn(activate);
        fighter.UnsubscribeToEndTurn(deactivate);
    }
}
