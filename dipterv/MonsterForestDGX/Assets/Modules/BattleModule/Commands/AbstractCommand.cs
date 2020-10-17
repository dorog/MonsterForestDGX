using System.Collections;
using UnityEngine;

public abstract class AbstractCommand : MonoBehaviour
{
    public Controller Controller;

    public void Execute()
    {
        StartCoroutine(nameof(ExecutingCommand));
    }

    private IEnumerator ExecutingCommand()
    {
        yield return ExecuteCommand();

        Controller.FinishedTheCommand();
    }

    protected abstract IEnumerator ExecuteCommand();
}
