
public class Gate : Fighter, IEnemy
{
    public BattleManager battleManager;

    public void Appear()
    {
        
    }

    public override void Die()
    {
        //TODO: Add animation
        gameObject.SetActive(false);
        battleManager.MonsterDied();
    }

    public void Disappear()
    {

    }

    public Health GetHealth()
    {
        return health;
    }

    public void React()
    {
        
    }

    public void ResetMonster()
    {
        health.ResetHealth();
    }

    public override void StartTurn()
    {
        battleManager.PlayerTurn();
    }
}
