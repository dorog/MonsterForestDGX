using UnityEngine;

public abstract class SpellCastingHandler : MonoBehaviour
{
    public abstract void Guess(Vector2 guess);
    public abstract RecognizingResult GetResult();
    public abstract void ResetHandler();
}
