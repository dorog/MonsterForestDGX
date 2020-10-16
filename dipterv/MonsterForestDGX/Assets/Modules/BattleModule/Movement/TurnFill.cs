using System.Collections;
using UnityEngine;

public abstract class TurnFill : MonoBehaviour
{
    public float time = 2;
    public float distance = 10;

    public Animator animator;

    public abstract IEnumerator Moving(bool forward, float time);
}
