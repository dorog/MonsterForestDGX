using System.Collections.Generic;
using UnityEngine;

public class TeleportUnlockerComponent : MonoBehaviour
{
    private IUnlockableTeleportManager teleportManager;

    private List<bool> teleportPoints;

    public void AddManager(IUnlockableTeleportManager _teleportManager)
    {
        teleportManager = _teleportManager;
        teleportManager.SubscribeToLoad(SetTeleportPoints);
        teleportManager.SubscribeToChanged(Refresh);
    }

    private void SetTeleportPoints(List<bool> _teleportPoints)
    {
        teleportPoints = _teleportPoints;
    }

    private void Refresh(int id)
    {
        teleportPoints[id] = !teleportPoints[id];
    }

    public void UnlockLocation(int id)
    {
        if (!teleportPoints[id])
        {
            teleportManager.ChangeLocationState(id);
        }
    }

    public void LockLocation(int id)
    {
        if (teleportPoints[id])
        {
            teleportManager.ChangeLocationState(id);
        }
    }
}
