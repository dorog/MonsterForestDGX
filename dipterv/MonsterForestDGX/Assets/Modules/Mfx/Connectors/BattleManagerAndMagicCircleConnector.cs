using UnityEngine;

public class BattleManagerAndMagicCircleConnector : MonoBehaviour
{
    public BattleManager battleManager;
    public MagicCircleHandler magicCircleHandler;

    public GameEvents gameEvents;

    void Start()
    {
        gameEvents.BattleStartDelegateEvent += Figth;
        gameEvents.BattleEndDelegateEvent += Explore;
    }

    private void Figth()
    {
        battleManager.BlueFighterTurnStartDelegateEvent += magicCircleHandler.AttackTurn;
        battleManager.RedFighterTurnStartDelegateEvent += magicCircleHandler.DefTurn;
    }

    private void Explore()
    {
        battleManager.BlueFighterTurnStartDelegateEvent -= magicCircleHandler.AttackTurn;
        battleManager.RedFighterTurnStartDelegateEvent -= magicCircleHandler.DefTurn;

        magicCircleHandler.DisableCasting();
    }
}
