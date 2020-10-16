using System;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    private event Action Died;

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
    public abstract void Fight();
    public abstract void Withdraw();
    public abstract void Win();
    public abstract void Draw();
}
