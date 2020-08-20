using UnityEngine.UI;

public class DollHealth : Health
{
    public TraningCampDamageUI damageUI;

    public DamageBlock damageBlock;

    private bool inBlock = false;

    public override void SetUpHealth()
    {

    }

    protected override float GetBlockedDamage(float dmg)
    {
        if (inBlock)
        {
            inBlock = false;

            damageUI.ShowDamage(damageBlock.GetCalculatedDamage(dmg));

            return 0;
        }
        else
        {
            damageUI.ShowDamage(dmg);

            return 0;
        }
    }

    public override void SetDamageBlock()
    {
        inBlock = true;
    }

    public override void TakeDamageBasedOnHit(float dmg, ElementType magicType, bool isHeadshot)
    {
        TakeDamage(dmg, magicType);
    }
}
