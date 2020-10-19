using UnityEngine;

public class TimeDamageBlock : DamageBlock
{
    public float durationTime = 2f;

    private bool working = false;
    private float actualDurationTime = 0;

    public override void StartBlocking()
    {
        actualDurationTime = durationTime;

        working = true;
    }

    private void Update()
    {
        if (working)
        {
            actualDurationTime -= Time.deltaTime;

            if (actualDurationTime <= 0)
            {
                working = false;
                BlockDowned();
            }
        }
    }

    public override float GetCalculatedDamage(float damage)
    {
        if (working)
        {
            return CalculateDamage(blockValue, damage);
        }

        return damage;
    }
}
