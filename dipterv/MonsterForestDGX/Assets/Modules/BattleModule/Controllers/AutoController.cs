
public class AutoController : Controller
{
    private bool isCancelled = false;

    public override void StartController()
    {
        isCancelled = false;
        base.StartController();
    }

    public override void StopController()
    {
        isCancelled = true;
        base.StopController();
    }

    public override void FinishedTheCommand()
    {
        base.FinishedTheCommand();
        if (!isCancelled)
        {
            base.StartController();
        }
    }
}
