
public class CoverageResult
{
    public string Name;
    public float Result;
    public float Min;

    public CoverageResult(string name, float result, float min)
    {
        Name = name;
        Result = result * 100;
        Min = min * 100;
    }
}
