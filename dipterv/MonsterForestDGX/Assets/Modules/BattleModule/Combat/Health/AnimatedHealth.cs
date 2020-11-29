using UnityEngine;

public class AnimatedHealth : Health
{
    [Header ("Monster Settings")]
    public string bodyHitAnimation;
    public string headHitAnimation;
    public string blockAnimation;
    public Animator animator;
    public string dieAnimation;

    public Shredding shredding;

    private string actualHitAnimation;

    private bool blockedLastOne = false;

    public override void InitHealth()
    {
        base.InitHealth();

        damageBlock.SubscribeToBlockDown(DamageBlockActivated);
    }

    public void TakeDamageBasedOnHit(float dmg, bool isHeadshot)
    {
        actualHitAnimation = isHeadshot ? headHitAnimation : bodyHitAnimation;

        MonsterTakeDamage(dmg);
    }

    private void MonsterTakeDamage(float dmg)
    {
        TakeDamage(dmg);

        if (blockedLastOne && currentHp > 0)
        {
            animator.SetTrigger(blockAnimation);
        }
        else if(currentHp > 0)
        {
            animator.SetTrigger(actualHitAnimation);
        }
        else
        {
            animator.SetTrigger(dieAnimation);
            shredding.Disappear();
        }
    }

    private void DamageBlockActivated()
    {
        blockedLastOne = true;
    }

    public override void DisableHealth()
    {
        base.DisableHealth();
        damageBlock.UnsubscribeToBlockDown(DamageBlockActivated);
    }
}
