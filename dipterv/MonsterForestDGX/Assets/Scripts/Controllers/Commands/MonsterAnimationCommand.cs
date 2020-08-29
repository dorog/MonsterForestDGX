using System.Collections;
using UnityEngine;

public class MonsterAnimationCommand : AbstractCommand
{
    public string animationStr;
    public Animator animator;

    protected override IEnumerator ExecuteCommand()
    {
        animator.SetTrigger(animationStr);

        return null;
    }
}
