using System;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    private event Action Died;

    private event Action StartTurn;
    private event Action EndTurn;

    public virtual void Die()
    {
        Died?.Invoke();
    }

    public void SubscribeToDie(Action method)
    {
        Died += method;
    }
    public void UnsubscribeToDie(Action method)
    {
        Died -= method;
    }

    public abstract void SetupForFight();
    public virtual void Fight()
    {
        StartTurn?.Invoke();
    }
    public virtual void Def()
    {
        EndTurn?.Invoke();
    }
    public abstract void Withdraw();
    public abstract void Win();
    public abstract void Draw();

    public void SubscribeToStartTurn(Action method)
    {
        StartTurn += method;
    }
    public void UnsubscribeToStartTurn(Action method)
    {
        StartTurn -= method;
    }
    public void SubscribeToEndTurn(Action method)
    {
        EndTurn += method;
    }
    public void UnsubscribeToEndTurn(Action method)
    {
        EndTurn -= method;
    }
}
