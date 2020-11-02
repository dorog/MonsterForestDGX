
public class TraningCampConnector : AbstractConnector
{
    public MfxPatternManager patternManager;
    public MfxTraningCampPatternComponent traningCampPatternComponent;

    public override void Setup()
    {
        traningCampPatternComponent.AddPatternManager(patternManager);
    }

    public override void Load() { }
}
