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
    public ResetableHandler resetable;

    public Health GetHealth()
    {
        return GetComponent<Health>();
    }

    protected override void React()
    {
        float random = Random.Range(1, 101);

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

    public override void Fight()
    {
        base.Fight();

        trainingCampUI.EnableUI();

        cooldownReset.Init(resetable.gameObject);
        magicCircleHandler.SuccessCastSpellDelegateEvent += cooldownReset.ResetCooldown;

        controller.StartController();
    }

    public override void Disable(){}
}
