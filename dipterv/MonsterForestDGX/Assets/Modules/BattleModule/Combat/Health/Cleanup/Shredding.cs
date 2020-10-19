using System.Collections;
using UnityEngine;

public class Shredding : MonoBehaviour
{
    public float amount = 10;
    public float disappearTime = 10;
    public float delayTime = 6;

    public IEnumerator Disappear()
    {
        Vector3 aimPosition = transform.position + amount * Vector3.down;

        yield return new WaitForSeconds(delayTime);

        StartCoroutine(EnumeratorMoving.MoveToPosition(transform, aimPosition, disappearTime));
    }
}
