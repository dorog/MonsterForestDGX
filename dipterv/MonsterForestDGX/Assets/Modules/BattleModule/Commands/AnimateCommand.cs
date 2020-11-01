using System.Collections;
using UnityEngine;

public class AnimateCommand : AbstractCommand
{
    public Animator animator;
    public float delay = 1;
    public float animationTime = 2;
    public string triggerParameter = "";

    protected override IEnumerator ExecuteCommand()
    {
        animator.SetTrigger(triggerParameter);

        yield return new WaitForSeconds(delay + animationTime);
    }
}
