
public class MockController : Controller
{
    public bool Finished { get; set; } = false;

    public override void FinishedTheCommand()
    {
        base.FinishedTheCommand();
        Finished = true;
    }
}
