using System.Collections;
using UnityEngine;

public class MoveCommand : AbstractCommand
{
    public TurnFill turnFill;
    public bool forward;

    protected override IEnumerator ExecuteCommand()
    {
        StartCoroutine(turnFill.Moving(forward));

        yield return new WaitForSeconds(turnFill.GetNecessaryTimeForMoving());
    }
}
