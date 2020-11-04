using System.Collections;
using UnityEngine;

public class TutorialCommand : AbstractCommand
{
    public GameObject previousUI;
    public GameObject ui;

    protected override IEnumerator ExecuteCommand()
    {
        SetupStep();
        Debug.Log(gameObject.name);

        yield return null;
    }

    protected virtual void SetupStep()
    {
        ui.SetActive(true);
        previousUI.SetActive(false);
    }
}
