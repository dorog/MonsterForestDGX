using System;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public Health health;

    public event Action Died;

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
}
