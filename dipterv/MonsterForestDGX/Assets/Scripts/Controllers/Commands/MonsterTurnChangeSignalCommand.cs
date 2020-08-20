using System.Collections;
using UnityEngine;

public class MonsterTurnChangeSignalCommand : AbstractCommand
{
    public bool playerTurn = false;

    public BattleManager battleManager;

    protected override IEnumerator ExecuteCommand()
    {
        if (playerTurn)
        {
            battleManager.PlayerTurn();
        }
        else
        {
            battleManager.MonsterTurn();
        }

        Controller.FinishedTheCommand();

        return null;
    }
}
