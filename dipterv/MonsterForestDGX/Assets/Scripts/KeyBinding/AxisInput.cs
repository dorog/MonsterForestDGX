using UnityEngine.XR;
using System;
using UnityEngine;

[Serializable]
public class AxisInput
{
    public XRNode device;
    public AxisInputType axis;

    public Vector2 GetValue()
    {
        InputDevice inputDevice = InputDevices.GetDeviceAtXRNode(device);
        inputDevice.TryGetFeatureValue(axis.GetUsage(), out Vector2 vector);

        return vector;
    }
}
