
public class MfxPatternShopConnector : AbstractConnector
{
    public PatternShopComponent patternShopComponent;
    public MfxPatternManager patternManager;

    public override void Setup()
    {
        patternShopComponent.AddPatternManager(patternManager);
    }

    public override void Load(){}
}
