using System.Collections.Generic;
using UnityEngine;

public class TeleportUnlockerComponent : MonoBehaviour
{
    private IUnlockableTeleportManager teleportManager;

    private List<IUnlockableTeleportPoint> teleportPoints;

    public void AddManager(IUnlockableTeleportManager _teleportManager)
    {
        teleportManager = _teleportManager;
        teleportManager.SubscribeToLoad(SetTeleportPoints);
        teleportManager.SubscribeToTargetLocationChanged(UnlockLocation);
    }

    private void SetTeleportPoints(List<IUnlockableTeleportPoint> _teleportPoints)
    {
        teleportPoints = _teleportPoints;
    }

    public void UnlockLocation(int id)
    {
        if (!teleportPoints[id].IsUnlocked())
        {
            teleportManager.UnlockLocation(id);
        }
    }
}
