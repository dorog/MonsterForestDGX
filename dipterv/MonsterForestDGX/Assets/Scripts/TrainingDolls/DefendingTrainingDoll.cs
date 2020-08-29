using UnityEngine;

public class DefendingTrainingDoll : MonoBehaviour, IEnemy
{
    public BattleManager battleManager;

    public Player player;

    public TrainingCampUI trainingCampUI;

    public CooldownResetPetAbility cooldownReset;

    [Range(0, 100)]
    public float blockChance;

    public DollHealth dollHealth;

    public Controller controller;

    public void Appear(){}

    public void Disappear(){}

    public Health GetHealth()
    {
        return GetComponent<Health>();
    }

    public void React()
    {
        float random = Random.Range(0, 101);

        if (random <= blockChance)
        {
            dollHealth.SetDamageBlock();
        }
    }

    public void ResetMonster(){}

    public void FinishedTraining()
    {
        trainingCampUI.DisableUI();

        cooldownReset.DisableAbility();

        player.FinishedTraining();
        battleManager.FinishedTraining();
    }

    public bool IsMonster()
    {
        return false;
    }

    public void Fight()
    {
        trainingCampUI.EnableUI();

        cooldownReset.Init(battleManager.player);

        controller.Step();
    }

    public void Disable()
    {
        
    }
}
