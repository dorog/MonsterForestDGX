
//TODO: Rename it, or add to other folder
public enum ExpType
{
    Cast, Hit, Kill
}

public static class ExpTypeMethods
{
    public static float GetExp(this ExpType expType)
    {
        switch (expType)
        {
            case ExpType.Cast:
                return 1;
            case ExpType.Hit:
                return 10;
            case ExpType.Kill:
                return 50;
            default:
                return 0;
        }
     }
}
