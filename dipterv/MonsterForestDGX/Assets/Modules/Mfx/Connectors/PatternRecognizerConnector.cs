
public class PatternRecognizerConnector : AbstractConnector
{
    public MfxPatternManager patternManager;
    public PatternRecognizerComponent patternRecognizerComponent;

    public override void Setup()
    {
        patternRecognizerComponent.AddPatternManager(patternManager);
    }

    public override void Load(){}
}
