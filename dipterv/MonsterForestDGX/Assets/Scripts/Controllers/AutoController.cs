
public class AutoController : Controller
{
    private bool isCancelled = false;

    public void StartController()
    {
        isCancelled = false;
        Step();
    }

    public void StopController()
    {
        ResetController();
        isCancelled = true;
    }

    public override void FinishedTheCommand()
    {
        base.FinishedTheCommand();
        if (!isCancelled)
        {
            Step();
        }
    }
}
