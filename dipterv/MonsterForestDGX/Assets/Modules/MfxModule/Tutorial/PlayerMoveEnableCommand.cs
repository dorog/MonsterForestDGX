
public class PlayerMoveEnableCommand : TutorialCommand
{
    public Player player;

    protected override void SetupStep()
    {
        base.SetupStep();

        player.EnableControlling();
    }
}
