
public enum MovingAxis
{
    X, Y, Z
}

public static class MovingAxisExtension
{
    public static MovingDirection GetPositiveDirection(this MovingAxis movingAxis, bool revert = false)
    {
        if (revert)
        {
            return GetNegtivDirection(movingAxis);
        }

        switch (movingAxis)
        {
            case MovingAxis.X:
                return MovingDirection.Right;
            case MovingAxis.Y:
                return MovingDirection.Up;
            case MovingAxis.Z:
                return MovingDirection.Forward;
            default:
                return MovingDirection.Forward;
        }
    }

    public static MovingDirection GetNegtivDirection(this MovingAxis movingAxis, bool revert = false)
    {
        if (revert)
        {
            return GetPositiveDirection(movingAxis);
        }

        switch (movingAxis)
        {
            case MovingAxis.X:
                return MovingDirection.Left;
            case MovingAxis.Y:
                return MovingDirection.Down;
            case MovingAxis.Z:
                return MovingDirection.Backward;
            default:
                return MovingDirection.Backward;
        }
    }
}
