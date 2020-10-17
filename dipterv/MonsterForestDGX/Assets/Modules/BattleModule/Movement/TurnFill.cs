using System.Collections;
using UnityEngine;

public abstract class TurnFill : MonoBehaviour
{
    public float distance = 10;

    public Animator animator;

    public abstract IEnumerator Moving(bool forward);
    public abstract float GetNecessaryTimeForMoving();
}
