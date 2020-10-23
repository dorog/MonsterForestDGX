
public class TeleportUiConnector : AbstractConnector
{
    public TeleportUiComponent teleportUI;
    public MfxTeleportManager teleportManager;

    public override void Setup()
    {
        teleportUI.AddManager(teleportManager);
    }

    public override void Load(){}
}
