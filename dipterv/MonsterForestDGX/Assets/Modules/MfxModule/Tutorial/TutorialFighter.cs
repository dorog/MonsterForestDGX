
public class TutorialFighter : PassiveFighter
{
    public Controller tutorialController;
    public Controller endController;

    public override void FightStarted()
    {
        base.FightStarted();

        tutorialController.StartController();
    }

    public override void Die()
    {
        base.Die();

        endController.StartController();
    }
}
