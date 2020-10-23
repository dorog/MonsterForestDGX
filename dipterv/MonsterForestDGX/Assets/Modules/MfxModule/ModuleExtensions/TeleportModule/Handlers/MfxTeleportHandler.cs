using UnityEngine;

public class MfxTeleportHandler : MonoBehaviour
{
    public DataManager dataManager;

    public MfxTeleportPoint[] teleportPoints;

    public MfxTeleportPoint[] GetTeleportPoints()
    {
        bool[] ports = dataManager.GetTeleportsState();

        for(int i = 0; i < ports.Length; i++)
        {
            teleportPoints[i].available = ports[i];
            teleportPoints[i].id = i;
        }

        return teleportPoints;
    }

    public int GetLastPositionId()
    {
        return dataManager.GetLastLocation();
    }

    public void SaveLastLocation(int id)
    {
        dataManager.SavePortLocation(id);
    }

    public void UnlockLocation(int id)
    {
        dataManager.SaveTeleportUnlock(id);
    }
}
