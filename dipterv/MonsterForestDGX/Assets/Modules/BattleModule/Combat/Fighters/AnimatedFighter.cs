using UnityEngine;

public class AnimatedFighter : PassiveFighter
{
    public Animator animator;
    public string dieAnimation;

    [Header ("Appear Settings")]
    public string appearAnimation;
    public float appearAnimationTime = 2;

    [Header("Disappear Settings")]
    public string disappearAnimation;
    public float disappearAnimationTime = 2;

    public AutoController EnemyDiedAutoController;

    public Shredding shredding;

    public override void Die()
    {
        animator.SetTrigger(dieAnimation);
        shredding.Disappear();

        base.Die();
    }

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
