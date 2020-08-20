using System.Collections;
using UnityEngine;

public class AutoController : AbstractController
{
    private bool isCancelled = false;

    public void StartController()
    {
        StartCoroutine("Running");
    }

    public IEnumerator Running()
    {
        while (!isCancelled)
        {
            Command cmd = GetNextCommand();
            cmd.Execute();

            yield return new WaitForSeconds(1);
        }
    }

    public void StopController()
    {
        isCancelled = true;
    }
}
