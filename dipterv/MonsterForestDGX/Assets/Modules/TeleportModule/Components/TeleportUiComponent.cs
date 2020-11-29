using System.Collections.Generic;
using UnityEngine;

public class TeleportUiComponent : MonoBehaviour
{
    private List<IUiTeleportPoint> teleportPoints;
    private IUiTeleportManager teleportManager;

    private int lastTargetLocation = -1;

    public void AddManager(IUiTeleportManager _teleportManager)
    {
        teleportManager = _teleportManager;
        teleportManager.SubscribeToLoad(SetTeleportPoints);
        teleportManager.SubscribeToChanged(RefreshUI);
        teleportManager.SubscribeToTargetLocationChanged(SetPortUI);
    }

    private void SetTeleportPoints(List<IUiTeleportPoint> _teleportPoints)
    {
        teleportPoints = _teleportPoints;
        SetupTeleportUI();
    }

    private void SetupTeleportUI()
    {
        for (int i = 0; i < teleportPoints.Count; i++)
        {
            teleportPoints[i].InstantiateUI();
        }
    }

    private void RefreshUI(int id)
    {
        if(lastTargetLocation == id && teleportPoints[id].GetState())
        {
            return;
        }
        teleportPoints[id].SetState(teleportPoints[id].GetState());
    }

    private void SetPortUI(int id)
    {
        if(lastTargetLocation != -1)
        {
            teleportPoints[lastTargetLocation].LeftUiLocation();
        }

        lastTargetLocation = id;
        teleportPoints[id].ReachedUiLocation();
    }
}
