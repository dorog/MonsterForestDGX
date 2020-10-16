using System;
using UnityEngine;

public abstract class Resetable : MonoBehaviour
{
    public abstract void ResetAction();
    public abstract void SubscribeToReset(Action method);
    public abstract void UnsubscribeToReset(Action method);
}
