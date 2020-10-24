
public class MockButtonInput : AbstractPressed
{
    private bool pressed = false;

    public void Pressed()
    {
        pressed = true;
    }

    protected override bool GetPressState()
    {
        if (pressed)
        {
            pressed = false;
            return true;
        }

        return false;
    }
}
