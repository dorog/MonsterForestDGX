using UnityEngine;

public class HitResult
{
    public bool Hit { get; set; }
    public int Id { get; set; }
    public Vector2 LastPoint { get; set; }
    public Vector2 StartPoint { get; set; }
    public bool Full { get; set; } = false;
}
