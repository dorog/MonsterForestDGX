using System;
using UnityEngine;

public abstract class MfxResetable : MonoBehaviour
{
    public abstract void ResetAction();
    public abstract void SubscribeToReset(Action method);
    public abstract void UnsubscribeToReset(Action method);
}
