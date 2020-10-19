using System.Collections;
using UnityEngine;

public class WaitCommand : AbstractCommand
{
    public float minWaitTime = 1;
    public float maxWaitTime = 10;

    protected override IEnumerator ExecuteCommand()
    {
        float waitTime = Random.Range(minWaitTime, maxWaitTime + 1);

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / waitTime;

            yield return null;
        }
    }
}
