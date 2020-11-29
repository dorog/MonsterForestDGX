
public class ShieldBooster : RewardEffect
{

    public float extraValue;
    public DamageBlock damageBlock;

    public override void Activate()
    {
        damageBlock.blockValue += extraValue;
    }
}
