using UnityEngine;

public class PlayerHealth : Health, ITarget
{
    [Header ("Extra Damage Block Settings")]
    public ParticleSystem shieldEffect;

    protected override float GetBlockedDamage(float dmg)
    {
        if (damageBlock != null)
        {
            return damageBlock.GetCalculatedDamage(dmg);
        }
        else
        {
            return dmg;
        }
    }

    public override void SetDamageBlock()
    {
        base.SetDamageBlock();

        shieldEffect.Play();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if (currentHp < 0)
        {
            InitHealth();
        }
    }
}
