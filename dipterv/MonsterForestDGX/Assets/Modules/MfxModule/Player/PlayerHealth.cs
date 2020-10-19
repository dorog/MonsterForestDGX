using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [Header ("Player Options")]
    public Text hp;
    public GameObject block;

    [Header ("Time Damage Block Settings")]
    public TimeDamageBlock timeDamageBlock;
    public GameObject blockUI;
    public ParticleSystem shieldEffect;

    public void BlockDown()
    {
        block.SetActive(false);
    }

    protected override float GetBlockedDamage(float dmg)
    {
        if (timeDamageBlock != null)
        {
            return timeDamageBlock.GetCalculatedDamage(dmg);
        }
        else
        {
            return dmg;
        }
    }

    public override void SetDamageBlock()
    {
        base.SetDamageBlock();

        blockUI.SetActive(true);
        shieldEffect.Play();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();

        if (currentHp < 0)
        {
            InitHealth();
        }
    }

    public void Full()
    {
        currentHp = maxHp;
        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }
}
