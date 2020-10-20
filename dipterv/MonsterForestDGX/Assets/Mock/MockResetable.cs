using System;

public class MockResetable : MfxResetable
{
    private event Action Reseting;

    public bool Reseted { get; set; } = false;

    public void ResetCall()
    {
        Reseting?.Invoke();
    }

    public override void ResetAction()
    {
        Reseted = true;
    }

    public override void SubscribeToReset(Action method)
    {
        Reseting += method;
    }

    public override void UnsubscribeToReset(Action method)
    {
        Reseting -= method;
    }
}
