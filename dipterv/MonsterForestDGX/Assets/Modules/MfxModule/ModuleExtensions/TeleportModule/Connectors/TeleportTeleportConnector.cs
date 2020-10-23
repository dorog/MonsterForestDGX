
public class TeleportTeleportConnector : AbstractConnector
{
    public BattleManager battleManager;
    public TeleporterComponent teleport;
    public MfxTeleportManager teleportManager;

    public override void Setup()
    {
        battleManager.RedFighterWon += teleport.TeleportToLastPosition;
        battleManager.Withdraw += teleport.TeleportToLastPosition;
        battleManager.Draw += teleport.TeleportToLastPosition;

        teleport.AddManager(teleportManager);
    }

    public override void Load(){}
}
