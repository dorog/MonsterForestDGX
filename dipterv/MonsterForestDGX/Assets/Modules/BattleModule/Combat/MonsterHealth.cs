using UnityEngine;

public class MonsterHealth : Health
{
    [Header ("Monster Settings")]
    public string bodyHitAnimation;
    public string headHitAnimation;
    public string blockAnimation;
    public Animator animator;
    public Disappearer monsterBodyDisappear;

    private bool death = false;

    public bool inBlock = false;

    public DamageBlock damageBlock;

    public HealthShowerUI healthShowerUI;

    private string actualHitAnimation;

    public void Init()
    {
        currentHp = maxHp;
        healthShowerUI.ShowHealthData(currentHp, maxHp);
    }

    public override void TakeDamageBasedOnHit(float dmg, ElementType magicType, bool isHeadshot)
    {
        actualHitAnimation = isHeadshot ? headHitAnimation : bodyHitAnimation;

        MonsterTakeDamage(dmg, magicType);

        if(healthShowerUI != null)
        {
            healthShowerUI.ShowHealthData(currentHp, maxHp);
        }
    }

    private void MonsterTakeDamage(float dmg, ElementType magicType)
    {
        if (inBlock)
        {
            TakeDamage(dmg, magicType);
            if(currentHp > 0)
            {
                animator.SetTrigger(blockAnimation);
            }
        }
        else
        {
            TakeDamage(dmg, magicType);
            if(currentHp > 0)
            {
                animator.SetTrigger(actualHitAnimation);
            }
        }
    }

    public override void TakeDamage(float dmg, ElementType magicType)
    {
        //Player doesn't need it?
        if (death)
        {
            return;
        }
        base.TakeDamage(dmg, magicType);
        if(currentHp <= 0)
        {
            death = true;
            StartCoroutine(monsterBodyDisappear.DisAppear());
        }
    }

    protected override float GetBlockedDamage(float dmg)
    {
        if (inBlock)
        {
            inBlock = false;
            return damageBlock.GetCalculatedDamage(dmg);
        }
        else
        {
            return dmg;
        }
    }

    public override void SetDamageBlock()
    {
        inBlock = true;
    }
}
