
public class MfxPatternData : PatternData
{
    public MfxPattern Pattern;

    public override IPattern GetPattern()
    {
        return Pattern;
    }
}
