
public class MfxPatternShopConnector : AbstractConnector
{
    public PatternShopComponent patternShopComponent;
    public PatternManager patternManager;

    public override void Setup()
    {
        patternShopComponent.AddPatternManager(patternManager);
    }

    public override void Load(){}
}
