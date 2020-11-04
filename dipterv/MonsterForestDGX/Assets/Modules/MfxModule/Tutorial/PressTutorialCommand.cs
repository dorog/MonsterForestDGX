using UnityEngine;

public class PressTutorialCommand : TutorialCommand
{
    public AbstractPressed button;
    public GameObject pressSuccessUI;

    protected override void SetupStep()
    {
        base.SetupStep();

        button.SubscribeToPressed(Pressed);

        button.Activate();
    }

    private void Pressed()
    {
        button.Deactivate();
        ui.SetActive(false);
        pressSuccessUI.SetActive(true);
    }
}
