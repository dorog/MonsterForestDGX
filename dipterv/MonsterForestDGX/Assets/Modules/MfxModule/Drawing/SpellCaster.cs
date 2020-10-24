using UnityEngine;

public abstract class SpellCaster : MonoBehaviour
{
    public abstract void CastBasedOnResult(RecognizingResult result);
}
