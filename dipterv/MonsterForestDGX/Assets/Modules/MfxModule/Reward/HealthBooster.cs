public class HealthBooster : RewardEffect
{
    public float extraHp;
    public Health health;

    public override void Activate()
    {
        health.maxHp += extraHp;
    }
}
