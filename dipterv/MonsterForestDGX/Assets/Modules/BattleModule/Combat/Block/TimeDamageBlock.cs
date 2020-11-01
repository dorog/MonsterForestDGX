using UnityEngine;

public class TimeDamageBlock : DamageBlock
{
    public float durationTime = 2f;

    private bool working = false;
    private float actualDurationTime = 0;

    [Header("(Optional)")]
    public ParticleSystem effect;

    public override void StartBlocking()
    {
        actualDurationTime = durationTime;

        if (effect != null)
        {
            effect.Play();
        }
        working = true;
    }

    public void Update()
    {
        if (working)
        {
            actualDurationTime -= Time.deltaTime;

            if (actualDurationTime <= 0)
            {
                working = false;
                if(effect != null)
                {
                    effect.Stop();
                }
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
