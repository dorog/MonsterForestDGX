
public class TeleportManagerConnector : AbstractConnector
{
    public MfxTeleportManager teleportManager;

    public override void Setup() { }

    public override void Load()
    {
        teleportManager.Load();
    }
}
