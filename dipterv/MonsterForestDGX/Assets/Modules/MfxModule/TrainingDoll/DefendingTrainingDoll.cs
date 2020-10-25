
using UnityEngine;

public class DefendingTrainingDoll : AiFighter
{
    public BattleManager battleManager;

    public TrainingCampUI trainingCampUI;

    public CooldownResetPetAbility cooldownReset;

    public ResetableHandler resetableHandler;

    public KeyBindingManager keyBindingManager;

    public Health GetHealth()
    {
        return GetComponent<Health>();
    }

    protected override void React(){}

    protected override void ResetMonster(){}

    protected override void Appear() { }

    protected override void Disappear() { }

    public void FinishedTraining()
    {
        trainingCampUI.DisableUI();

        battleManager.DrawFight();

        battleManager.BlueFighterTurnStartDelegateEvent -= keyBindingManager.drawHelperInput.Activate;
        battleManager.RedFighterTurnStartDelegateEvent -= keyBindingManager.drawHelperInput.Deactivate;
    }

    public override void SetupForFight(Fighter fighter)
    {
        base.SetupForFight(fighter);

        battleManager.BlueFighterTurnStartDelegateEvent += keyBindingManager.drawHelperInput.Activate;
        battleManager.RedFighterTurnStartDelegateEvent += keyBindingManager.drawHelperInput.Deactivate;
    }

    public override void Withdraw()
    {
        base.Withdraw();

        battleManager.BlueFighterTurnStartDelegateEvent -= keyBindingManager.drawHelperInput.Activate;
        battleManager.RedFighterTurnStartDelegateEvent -= keyBindingManager.drawHelperInput.Deactivate;
    }

    public override void Def()
    {
        base.Def();

        trainingCampUI.EnableUI();

        cooldownReset.Init(resetableHandler.gameObject);
    }

    public override void Disable(){}
}
