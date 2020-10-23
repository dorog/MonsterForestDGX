using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public int id;
    public TeleportUnlockerComponent teleportUnlocker;
    public MfxTeleportManager mfxTeleportManager;

    private void OnTriggerEnter(Collider other)
    {
        teleportUnlocker.UnlockLocation(id);
        mfxTeleportManager.LocationChanged(id);
    }
}
