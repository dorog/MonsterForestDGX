using System.Collections.Generic;
using UnityEngine;

public class TeleporterComponent : MonoBehaviour
{
    private int lastTeleportId = -1;
    private List<ITeleporterPoint> teleportPoints;
    private ITeleporterManager teleportManager;

    public Transform target;

    public void AddManager(ITeleporterManager _teleportManager)
    {
        teleportManager = _teleportManager;
        teleportManager.SubscribeToLoad(SetTeleportPoints);
        teleportManager.SubscribeToLastPositionIdLoad(SetTeleportLastPositionId);
        teleportManager.SubscribeToLastPositionIdLoad(TeleportTarget);
        teleportManager.SubscribeToTargetLocationChanged(SetTeleportLastPositionId);
    }

    private void SetTeleportPoints(List<ITeleporterPoint> _teleportPoints)
    {
        teleportPoints = _teleportPoints;
    }

    private void SetTeleportLastPositionId(int id)
    {
        lastTeleportId = id;
    }

    public void TeleportTarget(int locationId)
    {
        if (locationId != -1)
        {
            teleportPoints[locationId].TeleportTarget(target);
        }
    }

    public void TeleportToLastPosition()
    {
        TeleportTarget(lastTeleportId);
    }
}
