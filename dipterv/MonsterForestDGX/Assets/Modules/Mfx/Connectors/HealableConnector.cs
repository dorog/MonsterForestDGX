using System;
using UnityEngine;

public class HealableConnector : MonoBehaviour, IHealable
{
    public Health health;
    public GameEvents gameEvents;

    public void Heal(float amount)
    {
        health.Heal(amount);
    }

    public bool IsFull()
    {
        return health.IsFull();
    }

    public void SubscribeToHealEvents(Action activate, Action deactivate)
    {
        activate?.Invoke();
        gameEvents.BattleEndDelegateEvent += deactivate;
    }

    public void UnsubscribeFromHealEvents(Action activate, Action deactivate)
    {
        gameEvents.BattleEndDelegateEvent -= deactivate;
    }
}
