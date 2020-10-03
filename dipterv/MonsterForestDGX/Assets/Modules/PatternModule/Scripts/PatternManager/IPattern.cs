using UnityEngine;

public interface IPattern
{
    void Guess(Vector2 point);
    float GetResult();
    float GetMinCoverage();
    void ResetPattern();
    PatternState GetState();
}
