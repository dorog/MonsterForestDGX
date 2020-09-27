
using UnityEngine;

public abstract class PetUnlockSpot : MonoBehaviour
{
    public abstract void Refresh(bool available);
    public abstract void Setup(PetData _petData, int _id, PetUnlockerComponent _petUnlockerComponent);
}
