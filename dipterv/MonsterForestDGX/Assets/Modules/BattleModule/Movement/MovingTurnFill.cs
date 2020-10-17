using System.Collections;
using UnityEngine;

public class MovingTurnFill : TurnFill
{
    private int direction = -1;

    public float movementTime = 3f;

    [Header ("Animation Settings")]
    public string movingAnimation = "locomotion";
    public float forwardValue = 1f;
    public float backwardValue = 1f;

    public override IEnumerator Moving(bool forward)
    {
        direction = forward ? 1 : -1;
        float value = forward ? forwardValue : backwardValue;
        animator.SetFloat(movingAnimation, value * direction);

        Vector3 goalPosition = transform.position + distance * transform.forward * direction;

        StartCoroutine(EnumeratorMoving.MoveToPosition(transform, goalPosition, movementTime));

        yield return new WaitForSeconds(movementTime);

        animator.SetFloat(movingAnimation, 0);
    }

    public override float GetNecessaryTimeForMoving()
    {
        return movementTime;
    }
}
