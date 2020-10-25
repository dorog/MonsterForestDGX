using System;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    private event Action Attacking;
    private event Action Died;

    private event Action StartTurn;
    private event Action EndTurn;

    private Fighter enemy;

    private void SetEnemy(Fighter _enemy)
    {
        enemy = _enemy;

        enemy.SubscribeToAttack(React);
        enemy.SubscribeToDie(DefeatedEnemy);
    }

    private void DefeatedEnemy()
    {
        enemy.UnsubscribeToAttack(React);
        enemy.UnsubscribeToDie(DefeatedEnemy);
    }

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

    public virtual void SetupForFight(Fighter fighter)
    {
        SetEnemy(fighter);
    }

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

    protected abstract void React();

    public virtual void Attack()
    {
        Attacking?.Invoke();
    }

    public void SubscribeToAttack(Action method)
    {
        Attacking += method;
    }

    public void UnsubscribeToAttack(Action method)
    {
        Attacking -= method;
    }
}
