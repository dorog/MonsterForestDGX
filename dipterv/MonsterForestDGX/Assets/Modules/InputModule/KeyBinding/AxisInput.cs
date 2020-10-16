using UnityEngine.XR;
using System;
using UnityEngine;

public class AxisInput : MonoBehaviour
{
    public XRNode device;
    public AxisInputType axis;

    private bool active = true;
    private event Action<Vector2> AxisChange;

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

    private Vector2 GetValue()
    {
        InputDevice inputDevice = InputDevices.GetDeviceAtXRNode(device);
        inputDevice.TryGetFeatureValue(axis.GetUsage(), out Vector2 vector);

        return vector;
    }

    void Update()
    {
        if (active)
        {
            AxisChange?.Invoke(GetValue());
        }
    }
}
