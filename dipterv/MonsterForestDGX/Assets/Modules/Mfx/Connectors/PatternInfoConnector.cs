
public class PatternInfoConnector : AbstractConnector
{
    public PatternInfoComponent patternInfoComponent;
    public MfxPatternManager patternManager;

    public override void Setup()
    {
        patternInfoComponent.AddPatternManager(patternManager);
    }

    public override void Load() { }
}
