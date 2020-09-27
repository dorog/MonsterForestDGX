using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [Header ("Player Options")]
    public Text hp;
    public GameObject block;

    public TimeDamageBlock timeDamageBlock;

    public override void SetUpHealth()
    {
        base.SetUpHealth();
        
        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }

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

    public void Full()
    {
        currentHp = maxHp;
        hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
    }
}
