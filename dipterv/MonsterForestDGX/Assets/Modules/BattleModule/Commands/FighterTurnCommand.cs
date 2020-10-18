using System.Collections;
using UnityEngine;

public class FighterTurnCommand : AbstractCommand
{
    public Fighter fighter;

    public BattleManager battleManager;

    protected override IEnumerator ExecuteCommand()
    {
        battleManager.TurnChange(fighter);

        return null;
    }
}
