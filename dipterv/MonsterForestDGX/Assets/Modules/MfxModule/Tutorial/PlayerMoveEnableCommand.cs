
public class PlayerMoveEnableCommand : TutorialCommand
{
    public MovingHandler movingHandler;

    protected override void SetupStep()
    {
        base.SetupStep();

        movingHandler.EnableMovement();
    }
}
