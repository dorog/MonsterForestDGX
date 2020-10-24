using UnityEngine;
using UnityEngine.XR;

public class VrAxisInput : AxisInput
{
    public XRNode device;
    public AxisInputType axis;

    protected override Vector2 GetValue()
    {
        InputDevice inputDevice = InputDevices.GetDeviceAtXRNode(device);
        inputDevice.TryGetFeatureValue(axis.GetUsage(), out Vector2 vector);

        return vector;
    }

}
