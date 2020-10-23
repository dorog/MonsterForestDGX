
public class TeleportUnlockerConnector : AbstractConnector
{
    public TeleportUnlockerComponent teleportUnlocker;
    public MfxTeleportManager teleportManager;

    public override void Setup()
    {
        teleportUnlocker.AddManager(teleportManager);
    }

    public override void Load(){}
}
