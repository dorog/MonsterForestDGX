
public class UiPatternData : PatternData
{
    public IUiPattern UiPattern { get; set; }
    public override IPattern GetPattern()
    {
        return UiPattern;
    }
}
