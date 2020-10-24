using UnityEngine;

public abstract class SpellCastingHandler : MonoBehaviour
{
    public abstract void Guess(Vector3 guess);
    public abstract SpellResult GetResult();
    public abstract void ResetHandler();
}
