using UnityEngine;

public class DefendingTrainingDoll : AiFighter
{
    public GameObject uis;

    [Header ("Managers")]
    public RoundHandler roundHandler;
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

        roundHandler.SubscribeToStartTurn(keyBindingManager.drawHelperInput.Activate);
        roundHandler.SubscribeToEndTurn(keyBindingManager.drawHelperInput.Deactivate);
    }

    public override void Withdraw()
    {
        base.Withdraw();

        roundHandler.SubscribeToStartTurn(keyBindingManager.drawHelperInput.Activate);
        roundHandler.SubscribeToEndTurn(keyBindingManager.drawHelperInput.Deactivate);
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

        roundHandler.SubscribeToStartTurn(keyBindingManager.drawHelperInput.Activate);
        roundHandler.SubscribeToEndTurn(keyBindingManager.drawHelperInput.Deactivate);
    }
}
