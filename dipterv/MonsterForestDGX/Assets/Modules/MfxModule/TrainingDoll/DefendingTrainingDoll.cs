using UnityEngine;

public class DefendingTrainingDoll : PassiveFighter
{
    public GameObject uis;

    [Header ("Managers")]
    public BattleManager battleManager;
    public KeyBindingManager keyBindingManager;

    [Header ("Reset Settings")]
    public CooldownResetPetAbility cooldownReset;
    public ResetableHandler resetableHandler;

    public void FinishedTraining()
    {
        battleManager.DrawFight();
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

    public override void FightStarted()
    {
        base.FightStarted();

        uis.SetActive(true);

        cooldownReset.Init(resetableHandler.gameObject);
    }

    public override void Draw()
    {
        base.Draw();

        uis.SetActive(false);

        battleManager.BlueFighterTurnStartDelegateEvent -= keyBindingManager.drawHelperInput.Activate;
        battleManager.RedFighterTurnStartDelegateEvent -= keyBindingManager.drawHelperInput.Deactivate;
    }
}
