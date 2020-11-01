using UnityEngine.XR;

public class VrButtonInput : AbstractPressed
{
    public XRNode device;
    public ButtonInputType Button;

    protected override bool GetPressState()
    {
        InputDevice inputDevice = InputDevices.GetDeviceAtXRNode(device);
        inputDevice.TryGetFeatureValue(Button.GetUsage(), out bool value);

        return value;
    }
}
