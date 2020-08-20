using System.Collections;
using UnityEngine;

public abstract class AbstractCommand : MonoBehaviour
{
    public Controller Controller;

    public void Execute()
    {
        StartCoroutine(nameof(ExecuteCommand));
    }

    protected abstract IEnumerator ExecuteCommand();
}
