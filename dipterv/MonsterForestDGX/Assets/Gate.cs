
public class Gate : Fighter, IEnemy
{
    public BattleManager battleManager;
    public int id;

    public Controller controller;

    public override void Die()
    {
        //TODO: Add animation
        gameObject.SetActive(false);
        battleManager.MonsterDied();
    }

    public void Fight()
    {
        controller.Step();
    }

    public Health GetHealth()
    {
        return health;
    }

    public bool IsMonster()
    {
        return false;
    }

    public void ResetMonster()
    {
        health.ResetHealth();
    }

    public void Appear(){}

    public void Disappear(){}
}
