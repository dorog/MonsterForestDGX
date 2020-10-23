using System;
using System.Collections.Generic;
using UnityEngine;

public class MfxTeleportManager : MonoBehaviour, ITeleporterManager, IUiTeleportManager, IUnlockableTeleportManager
{
    public MfxTeleportHandler teleportHandler;

    private event Action<List<ITeleporterPoint>> LoadedTeleportTeleports;
    private event Action<List<IUiTeleportPoint>> LoadedUiTeleports;
    private event Action<List<IUnlockableTeleportPoint>> LoadedUnlockableTeleports;
    private event Action<int> LoadedLastPosition;
    private event Action<int> Changed;
    private event Action<int> ChangedLocation;

    private MfxTeleportPoint[] teleportPoints;

    public void Load()
    {
        teleportPoints = teleportHandler.GetTeleportPoints();
        int lastPositionId = teleportHandler.GetLastPositionId();

        List<ITeleporterPoint> teleportTeleportPoints = new List<ITeleporterPoint>();
        List<IUiTeleportPoint> uiTeleportPoints = new List<IUiTeleportPoint>();
        List<IUnlockableTeleportPoint> unlockableTeleportPoints = new List<IUnlockableTeleportPoint>();

        foreach(var item in teleportPoints)
        {
            teleportTeleportPoints.Add(item);
            uiTeleportPoints.Add(item);
            unlockableTeleportPoints.Add(item);
        }

        LoadedTeleportTeleports?.Invoke(teleportTeleportPoints);
        LoadedUiTeleports?.Invoke(uiTeleportPoints);
        LoadedUnlockableTeleports?.Invoke(unlockableTeleportPoints);
        LoadedLastPosition?.Invoke(lastPositionId);
    }

    public void LocationChanged(int id)
    {
        teleportHandler.SaveLastLocation(id);

        ChangedLocation?.Invoke(id);
    }

    public void UnlockLocation(int id)
    {
        teleportPoints[id].available = true;
        teleportHandler.UnlockLocation(id);

        Changed?.Invoke(id);
    }

    public void SubscribeToLoad(Action<List<ITeleporterPoint>> method)
    {
        LoadedTeleportTeleports += method;
    }

    public void SubscribeToLastPositionIdLoad(Action<int> method)
    {
        LoadedLastPosition += method;
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

    public void SubscribeToLoad(Action<List<IUnlockableTeleportPoint>> method)
    {
        LoadedUnlockableTeleports += method;
    }
}
