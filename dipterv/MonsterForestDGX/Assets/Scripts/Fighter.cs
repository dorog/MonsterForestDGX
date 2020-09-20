using UnityEngine;

public abstract class Fighter : MonoBehaviour//, IBattleParticipate
{
    public Health health;

    public abstract void Die();

    /*public abstract void FinishFight();

    public virtual void PrepareForFight(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }*/
}
