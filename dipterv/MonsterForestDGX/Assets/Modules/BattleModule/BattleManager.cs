using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Fighter redFighter;
    private Fighter blueFighter;

    public event Action BlueFighterTurnStartDelegateEvent;
    public event Action BlueFighterTurnEndDelegateEvent;
    public event Action RedFighterTurnStartDelegateEvent;
    public event Action RedFighterTurnEndDelegateEvent;

    public event Action BlueFighterWon;
    public event Action RedFighterWon;
    public event Action Draw;
    public event Action Withdraw;

    public Controller controller;

    public void BattleLobby(Fighter _redFighter, Fighter _blueFighter)
    {
        redFighter = _redFighter;
        blueFighter = _blueFighter;

        redFighter.SubscribeToDie(RedFighterDied);
        blueFighter.SubscribeToDie(BlueFighterDied);

        redFighter.SetupForFight(blueFighter);
        blueFighter.SetupForFight(redFighter);
    }

    public void BattleStart()
    {
        controller.StartController();
    }

    public void TurnChange(Fighter fighter)
    {
        if(redFighter == fighter)
        {
            RedFighterTurn();
        }
        else if(blueFighter == fighter)
        {
            BlueFighterTurn();
        }
    }

    private void BlueFighterTurn()
    {
        BlueFighterTurnStartDelegateEvent?.Invoke();
        RedFighterTurnEndDelegateEvent?.Invoke();
    }

    private void RedFighterTurn()
    {
        BlueFighterTurnEndDelegateEvent?.Invoke();
        RedFighterTurnStartDelegateEvent?.Invoke();
    }

    public void RedFighterDied()
    {
        blueFighter.Win();

        BlueFighterWon?.Invoke();

        FightOver();
    }

    public void BlueFighterDied()
    {
        redFighter.Win();

        RedFighterWon?.Invoke();

        FightOver();
    }

    private void FightOver()
    {
        controller.StopController();
        UnsubscribeDieFromDieEvents();
    }

    private void UnsubscribeDieFromDieEvents()
    {
        blueFighter.UnsubscribeToDie(BlueFighterDied);
        redFighter.UnsubscribeToDie(RedFighterDied);
    }

    public void WithdrawFromFight()
    {
        redFighter.Withdraw();
        blueFighter.Withdraw();

        Withdraw?.Invoke();
    }

    public void DrawFight()
    {
        redFighter.Draw();
        blueFighter.Draw();

        Draw?.Invoke();

        FightOver();
    }
}
