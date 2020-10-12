using UnityEngine;

public class DefendingTrainingDoll : AiFighter
{
    public BattleManager battleManager;

    public Player player;

    public TrainingCampUI trainingCampUI;

    public CooldownResetPetAbility cooldownReset;

    [Range(0, 100)]
    public float blockChance;

    public DollHealth dollHealth;

    public Controller controller;

    protected override void Appear(){}

    protected override void Disappear(){}

    public MagicCircleHandler magicCircleHandler;

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

    protected override void ResetMonster(){}

    public void FinishedTraining()
    {
        player.FinishedTraining();

        trainingCampUI.DisableUI();

        magicCircleHandler.SuccessCastSpellDelegateEvent -= cooldownReset.ResetCooldown;

        battleManager.DrawFight();
    }

    public override EnemyType IsMonster()
    {
        return EnemyType.Dummy;
    }

    public override void Fight()
    {
        trainingCampUI.EnableUI();

        cooldownReset.Init(new PetParameter() { Resetable = magicCircleHandler });
        magicCircleHandler.SuccessCastSpellDelegateEvent += cooldownReset.ResetCooldown;

        controller.Step();
    }

    public override void Disable()
    {
        
    }
}
