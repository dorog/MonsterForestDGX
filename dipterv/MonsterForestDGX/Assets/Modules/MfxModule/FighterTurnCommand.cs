using System.Collections;
using UnityEngine;

public class FighterTurnCommand : AbstractCommand
{
    public Fighter fighter;

    public RoundHandler roundHandler;
    public bool isPlayerTurn;

    protected override IEnumerator ExecuteCommand()
    {
        if (isPlayerTurn)
        {
            roundHandler.Fight();
        }
        else
        {
            roundHandler.Def();
        }

        return null;
    }
}
