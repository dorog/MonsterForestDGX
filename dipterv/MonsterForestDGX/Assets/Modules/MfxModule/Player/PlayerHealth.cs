using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [Header ("Player Options")]
    public Text hp;
    public GameObject block;

    public TimeDamageBlock timeDamageBlock;

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
        if (timeDamageBlock != null)
        {
            timeDamageBlock.StartBlock();
        }
    }

    public override void TakeDamageBasedOnHit(float dmg, ElementType magicType, bool isHeadshot)
    {
        
    }

    public override void TakeDamage(float dmg, ElementType magicType)
    {
        base.TakeDamage(dmg, magicType);

        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();

        if (currentHp < 0)
        {
            ResetHealth();
        }
    }

    public void Full()
    {
        currentHp = maxHp;
        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }
}
