using System.Collections;
using UnityEngine;

public class MonsterMoveCommand : AbstractCommand
{
    public TurnFill turnFill;
    public bool forward;

    protected override IEnumerator ExecuteCommand()
    {
        StartCoroutine(turnFill.Moving(forward, turnFill.time));

        yield return new WaitForSeconds(turnFill.time);

        Controller.FinishedTheCommand();
    }
}
