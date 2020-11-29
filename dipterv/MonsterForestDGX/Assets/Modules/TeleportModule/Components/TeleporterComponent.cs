using System.Collections.Generic;
using UnityEngine;

public class TeleporterComponent : MonoBehaviour
{
    private int lastTeleportId = -1;
    private List<ITeleporterPoint> teleportPoints;
    private ITeleporterManager teleportManager;

    public TeleportLocationSaveHandler teleportLocationSaveHandler;

    public Transform target;

    public void AddManager(ITeleporterManager _teleportManager)
    {
        teleportManager = _teleportManager;
        teleportManager.SubscribeToLoad(SetTeleportPoints);
    }

    private void SetTeleportPoints(List<ITeleporterPoint> _teleportPoints)
    {
        teleportPoints = _teleportPoints;

        if(teleportLocationSaveHandler != null)
        {
            lastTeleportId = teleportLocationSaveHandler.GetLastPositionId();

            TeleportTarget(lastTeleportId);
        }
    }

    public void TeleportTarget(int locationId)
    {
        if (locationId != -1 && locationId < teleportPoints.Count && teleportPoints[locationId].GetState())
        {
            teleportPoints[locationId].TeleportTarget(target);
        }
    }

    public void TeleportToLastPosition()
    {
        TeleportTarget(lastTeleportId);
    }
}
