using System.Collections;
using UnityEngine;

public class EnumeratorMoving
{
    public static IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove, float delayTime = 0f)
    {
        yield return new WaitForSeconds(delayTime);

        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
