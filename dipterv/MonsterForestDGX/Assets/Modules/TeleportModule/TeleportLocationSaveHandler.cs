using UnityEngine;

public abstract class TeleportLocationSaveHandler : MonoBehaviour
{
    public abstract int GetLastPositionId();
    public abstract void SaveLastLocation(int id);
}
