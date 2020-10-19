using System.Collections;
using UnityEngine;

public abstract class TurnFill : MonoBehaviour
{
    public float distance = 10;

    public float delay = 0f;

    public IEnumerator Move(MovingDirection direction)
    {
        yield return new WaitForSeconds(delay);

        StartCoroutine(Moving(direction));
    }

    protected abstract IEnumerator Moving(MovingDirection direction);
    public abstract float GetNecessaryTimeForMoving();
}
