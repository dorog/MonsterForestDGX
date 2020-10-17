using System.Collections;
using UnityEngine;

public class AppearTurnFill : TurnFill
{
    public float undergroundtime;

    [Header ("Appear Settings")]
    public string appearAnimation;
    public float appearAnimationTime;
    [Header("Disappear Settings")]
    public string disapperAnimation;
    public float disappearAnimationTime;

    public override IEnumerator Moving(bool forward)
    {
        animator.SetTrigger(disapperAnimation);
        yield return new WaitForSeconds(disappearAnimationTime);

        transform.position += transform.forward * distance * (forward ? 1 : -1);
        yield return new WaitForSeconds(undergroundtime);

        animator.SetTrigger(appearAnimation);
        yield return new WaitForSeconds(appearAnimationTime);
    }

    public override float GetNecessaryTimeForMoving()
    {
        return undergroundtime + appearAnimationTime + disappearAnimationTime;
    }
}
