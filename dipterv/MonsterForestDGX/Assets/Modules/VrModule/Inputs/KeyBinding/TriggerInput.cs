using System;
using UnityEngine;
using UnityEngine.XR;

[Serializable]
public class TriggerInput : AbstractPressed
{
    public XRNode device;
    public TriggerInputType trigger;
    [Range (0, 1)]
    public float pressLimit = 0.1f;

    protected override bool GetPressState()
    {
        InputDevice inputDevice = InputDevices.GetDeviceAtXRNode(device);
        inputDevice.TryGetFeatureValue(trigger.GetUsage(), out float value);

        return pressLimit <= value;
    }
}
