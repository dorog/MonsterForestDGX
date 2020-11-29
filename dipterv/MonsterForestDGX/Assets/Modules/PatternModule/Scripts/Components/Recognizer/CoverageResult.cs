
public class CoverageResult
{
    public int Id { get; set; }
    public float Result { get; set; }
    public float Min { get; set; }

    public CoverageResult(int id, float result, float min)
    {
        Id = id;
        Result = result * 100;
        Min = min * 100;
    }
}
