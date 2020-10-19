using System.Collections;
using UnityEngine;

public class AnimatedMovingTurnFill : MovingTurnFill
{
    [Header ("Animation Settings")]
    public Animator animator;
    public string movingAnimation = "locomotion";
    [Range (0, 1)]
    public float positiveValue = 1f;
    [Range (-1, 0)]
    public float negativValue = -1f;

    public MovingAxis movingAxis;
    public bool revert = false;

    protected override IEnumerator Moving(MovingDirection direction)
    {
        float value = 0;
        if (direction == movingAxis.GetPositiveDirection(revert))
        {
            value = positiveValue;
        }
        else if (direction == movingAxis.GetNegtivDirection(revert))
        {
            value = negativValue;
        }

        animator.SetFloat(movingAnimation, value);

        StartCoroutine(base.Moving(direction));

        yield return new WaitForSeconds(movementTime);

        animator.SetFloat(movingAnimation, 0);
    }
}