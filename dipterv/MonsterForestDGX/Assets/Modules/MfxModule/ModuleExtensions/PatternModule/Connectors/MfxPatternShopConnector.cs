
public class MfxPatternShopConnector : AbstractConnector
{
    public ShopComponent patternShopComponent;
    public MfxPatternManager patternManager;

    public override void Setup()
    {
        patternShopComponent.AddPatternManager(patternManager);
    }

    public override void Load(){}
}
