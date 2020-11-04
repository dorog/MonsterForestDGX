using System.Collections;
using UnityEngine;

public class FighterTurnCommand : AbstractCommand
{
    public Fighter fighter;

    public BattleManager battleManager;

    protected override IEnumerator ExecuteCommand()
    {
        Debug.Log(fighter.gameObject.name);

        battleManager.TurnChange(fighter);

        return null;
    }
}
