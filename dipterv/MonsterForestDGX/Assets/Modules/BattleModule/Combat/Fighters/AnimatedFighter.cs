using UnityEngine;

public class AnimatedFighter : AiFighter
{
    public Animator animator;

    [Header ("Appear Settings")]
    public string appearAnimation;
    public float appearAnimationTime = 2;

    [Header("Disappear Settings")]
    public string disappearAnimation;
    public float disappearAnimationTime = 2;

    public AutoController EnemyDiedAutoController;

    protected override void Appear()
    {
        base.Appear();

        animator.SetTrigger(appearAnimation);
    }

    protected override void Disappear()
    {
        base.Disappear();

        animator.SetTrigger(disappearAnimation);
    }

    protected override void ResetMonster()
    {
        base.ResetMonster();

        EnemyDiedAutoController.StartController();
    }
}
