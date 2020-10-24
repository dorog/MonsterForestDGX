using System;
using UnityEngine;

public abstract class AxisInput : MonoBehaviour
{
    private event Action<Vector2> AxisChange;
    private bool active = true;

    public void SubscibeToAxisChange(Action<Vector2> method)
    {
        AxisChange += method;
    }

    public void UnsubscibeToAxisChange(Action<Vector2> method)
    {
        AxisChange -= method;
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    protected abstract Vector2 GetValue();

    void Update()
    {
        if (active)
        {
            AxisChange?.Invoke(GetValue());
        }
    }
}
