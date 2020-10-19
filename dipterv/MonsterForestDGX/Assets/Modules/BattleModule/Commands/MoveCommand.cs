using System.Collections;
using UnityEngine;

public class MoveCommand : AbstractCommand
{
    public TurnFill turnFill;
    public MovingDirection direction;

    protected override IEnumerator ExecuteCommand()
    {
        StartCoroutine(turnFill.Move(direction));

        yield return new WaitForSeconds(turnFill.GetNecessaryTimeForMoving());
    }
}
