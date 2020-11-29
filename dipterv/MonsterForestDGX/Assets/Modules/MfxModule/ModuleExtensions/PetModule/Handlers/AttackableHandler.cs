using System;
using UnityEngine;

public class AttackableHandler : MonoBehaviour, IAttackable
{
    public Health health;
    public RoundHandler roundHandler;

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
        roundHandler.SubscribeToStartTurn(activate);
        roundHandler.SubscribeToEndTurn(deactivate);
    }

    public void UnsubscribeFromAttackEvents(Action activate, Action deactivate)
    {
        roundHandler.UnsubscribeToStartTurn(activate);
        roundHandler.UnsubscribeToEndTurn(deactivate);
    }
}
