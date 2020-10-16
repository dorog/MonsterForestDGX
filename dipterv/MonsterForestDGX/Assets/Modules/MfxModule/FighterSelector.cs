using UnityEngine;

public abstract class FighterSelector : MonoBehaviour
{
    public abstract Fighter GetBlueFighter();
    public abstract Fighter GetRedFighter();
}
