using UnityEngine;

public class BattleConnector : MonoBehaviour
{
    public BattleManager battleManager;

    private GameEvents gameEvents;

    public void Start()
    {
        gameEvents.BattleLobbyEnteredDelegateEvent += BattleLobby;

        battleManager.Draw += Explore;
        battleManager.BlueFighterWon += Explore;
        battleManager.RedFighterWon += Explore;
    }

    private void BattleLobby()
    {
        battleManager.BattleLobby(gameEvents.GetRedFighter(), gameEvents.GetBlueFighter());
    }

    private void Explore()
    {
        gameEvents.Explore();
    }

    public void Fight()
    {
        gameEvents.Fight();
        battleManager.BattleStart();
    }
}
