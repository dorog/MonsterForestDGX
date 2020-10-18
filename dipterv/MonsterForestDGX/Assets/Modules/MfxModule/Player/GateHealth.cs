public class GateHealth : Health
{
    public GateElement[] gateElements;

    private void Awake()
    {
        maxHp = gateElements.Length;
    }

    public override void SetDamageBlock()
    {
        
    }

    protected override float GetBlockedDamage(float dmg)
    {
        return dmg;
    }

    public override void ResetHealth()
    {
        base.ResetHealth();
        foreach(var gateElement in gateElements)
        {
            //TODO: Remove if gateElements has animation
            gateElement.gameObject.SetActive(true);
            gateElement.ResetElement();
        }
    }

    public override void TakeDamageBasedOnHit(float dmg, bool isHeadshot){}

    public void HideCrystals()
    {
        foreach(var gateElement in gateElements)
        {
            gateElement.gameObject.SetActive(false);
        }
    }
}
