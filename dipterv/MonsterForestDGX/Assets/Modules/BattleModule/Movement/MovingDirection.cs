using UnityEngine;

public enum MovingDirection
{
    Forward, Backward, Up, Down, Left, Right
}

public static class MovingDirectionExtension
{
    public static Vector3 GetDirection(this MovingDirection movingDirection, Transform transform)
    {
        switch (movingDirection)
        {
            case MovingDirection.Forward:
                return transform.forward;
            case MovingDirection.Backward:
                return transform.forward * -1;
            case MovingDirection.Up:
                return transform.up;
            case MovingDirection.Down:
                return transform.up * -1;
            case MovingDirection.Left:
                return transform.right * -1;
            case MovingDirection.Right:
                return transform.right;
            default:
                return Vector3.zero;
        }
    }
}
