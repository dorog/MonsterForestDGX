
public class UiPatternData : PatternData
{
    public UiPattern UiPattern { get; set; }
    public override IPattern GetPattern()
    {
        return UiPattern;
    }
}
