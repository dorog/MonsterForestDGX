
public abstract class PatternData
{
    public PatternState State { get; set; }
    public abstract IPattern GetPattern();
}
