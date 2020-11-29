using System;
using System.Collections.Generic;
using UnityEngine;

public class MfxTeleportManager : MonoBehaviour, ITeleporterManager, IUiTeleportManager, IUnlockableTeleportManager
{
    public MfxTeleportHandler teleportHandler;

    private event Action<List<ITeleporterPoint>> LoadedTeleportTeleports;
    private event Action<List<IUiTeleportPoint>> LoadedUiTeleports;
    private event Action<List<bool>> LoadedUnlockableTeleports;
    private event Action<int> Changed;
    private event Action<int> ChangedLocation;

    private MfxTeleportPoint[] teleportPoints;

    public void Load()
    {
        List<ITeleporterPoint> teleportTeleportPoints = new List<ITeleporterPoint>();
        List<IUiTeleportPoint> uiTeleportPoints = new List<IUiTeleportPoint>();
        List<bool> unlockableTeleportPoints = new List<bool>();

        foreach(var item in teleportPoints)
        {
            teleportTeleportPoints.Add(item);
            uiTeleportPoints.Add(item);
            unlockableTeleportPoints.Add(item.GetState());
        }

        LoadedTeleportTeleports?.Invoke(teleportTeleportPoints);
        LoadedUiTeleports?.Invoke(uiTeleportPoints);
        LoadedUnlockableTeleports?.Invoke(unlockableTeleportPoints);
    }

    public void LocationChanged(int id)
    {
        ChangedLocation?.Invoke(id);
    }

    public void ChangeLocationState(int id)
    {
        teleportPoints[id].SetState(!teleportPoints[id].GetState());

        teleportHandler.UnlockLocation(id, teleportPoints[id].GetState());

        Changed?.Invoke(id);
    }

    public void SubscribeToLoad(Action<List<ITeleporterPoint>> method)
    {
        LoadedTeleportTeleports += method;
    }

    public void SubscribeToChanged(Action<int> method)
    {
        Changed += method;
    }

    public void SubscribeToTargetLocationChanged(Action<int> method)
    {
        ChangedLocation += method;
    }

    public void SubscribeToLoad(Action<List<IUiTeleportPoint>> method)
    {
        LoadedUiTeleports += method;
    }

    public void SubscribeToLoad(Action<List<bool>> method)
    {
        LoadedUnlockableTeleports += method;
    }
}
