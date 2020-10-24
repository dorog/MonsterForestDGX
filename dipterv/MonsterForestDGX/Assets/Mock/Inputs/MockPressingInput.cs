
public class MockPressingInput : AbstractPressed
{
    private bool Pressing = false;

    public void PressingActivate()
    {
        Pressing = true;
    }

    public void PressingDeactivate()
    {
        Pressing = false;
    }

    protected override bool GetPressState()
    {
        return Pressing;
    }
}
