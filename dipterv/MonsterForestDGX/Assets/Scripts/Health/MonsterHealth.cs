using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : Health
{
    public string bodyHitAnimation;
    public string headHitAnimation;
    public string blockAnimation;
    public Animator animator;
    public MonsterBodyDisappear monsterBodyDisappear;

    private bool death = false;

    public bool inBlock = false;

    public DamageBlock damageBlock;

    public Text hp;

    private string actualHitAnimation;

    public override void SetUpHealth()
    {
        base.SetUpHealth();

        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }

    public override void TakeDamageBasedOnHit(float dmg, ElementType magicType, bool isHeadshot)
    {
        actualHitAnimation = isHeadshot ? headHitAnimation : bodyHitAnimation;

        MonsterTakeDamage(dmg, magicType);
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
            //hpSlider.gameObject.SetActive(false);
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
