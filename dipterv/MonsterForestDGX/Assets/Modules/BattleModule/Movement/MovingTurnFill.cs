using System.Collections;
using UnityEngine;

public class MovingTurnFill : TurnFill
{
    public float movementTime = 3f;

    public override float GetNecessaryTimeForMoving()
    {
        return movementTime;
    }

    protected override IEnumerator Moving(MovingDirection direction)
    {
        Vector3 goalPosition = transform.position + distance * direction.GetDirection(transform);

        StartCoroutine(EnumeratorMoving.MoveToPosition(transform, goalPosition, movementTime));

        yield return new WaitForSeconds(movementTime);
    }
}
