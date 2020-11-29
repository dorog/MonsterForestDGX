using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    protected Fighter redFighter;
    protected Fighter blueFighter;

    public event Action BlueFighterWon;
    public event Action RedFighterWon;
    public event Action Draw;
    public event Action Withdraw;

    public Controller controller;

    public virtual void BattleLobby(Fighter _redFighter, Fighter _blueFighter)
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
        redFighter.FightStarted();
        blueFighter.FightStarted();

        controller.StartController();
    }

    public virtual void RedFighterDied()
    {
        blueFighter.Win();

        BlueFighterWon?.Invoke();

        FightOver();
    }

    public virtual void BlueFighterDied()
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

    public virtual void DrawFight()
    {
        redFighter.Draw();
        blueFighter.Draw();

        Draw?.Invoke();

        FightOver();
    }
}
