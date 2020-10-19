using System.Collections;
using UnityEngine;

public class AppearTurnFill : TurnFill
{
    public float undergroundtime;

    public Animator animator;

    [Header ("Appear Settings")]
    public string appearAnimation;
    public float appearAnimationTime;
    [Header("Disappear Settings")]
    public string disapperAnimation;
    public float disappearAnimationTime;

    protected override IEnumerator Moving(MovingDirection direction)
    {
        animator.SetTrigger(disapperAnimation);
        yield return new WaitForSeconds(disappearAnimationTime);

        transform.position += direction.GetDirection(transform) * distance;
        yield return new WaitForSeconds(undergroundtime);

        animator.SetTrigger(appearAnimation);
        yield return new WaitForSeconds(appearAnimationTime);
    }

    public override float GetNecessaryTimeForMoving()
    {
        return undergroundtime + appearAnimationTime + disappearAnimationTime;
    }
}
