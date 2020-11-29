
public class MfxTeleportHandler : TeleportLocationSaveHandler
{
    public DataManager dataManager;

    public MfxTeleportPoint[] teleportPoints;

    public MfxTeleportPoint[] GetTeleportPoints()
    {
        bool[] ports = dataManager.GetTeleportsState();

        for(int i = 0; i < ports.Length; i++)
        {
            teleportPoints[i].SetState(ports[i]);
            teleportPoints[i].id = i;
        }

        return teleportPoints;
    }

    public override int GetLastPositionId()
    {
        return dataManager.GetLastLocation();
    }

    public override void SaveLastLocation(int id)
    {
        dataManager.SavePortLocation(id);
    }

    public void UnlockLocation(int id, bool state)
    {
        dataManager.SaveTeleportUnlock(id, state);
    }
}
