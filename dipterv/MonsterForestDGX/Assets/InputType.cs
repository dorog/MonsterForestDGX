using UnityEngine;
using UnityEngine.XR;

public enum ButtonInputType
{
    PrimaryBtn, SecondaryBtn
}

public enum AxisInputType
{
    Primary2DAxis, Secondary2DAxis
}

public enum TriggerInputType
{
    TriggerButton, GripButton
}

public static class InputTypeExtensions
{
    public static InputFeatureUsage<bool> GetUsage(this ButtonInputType inputType)
    {
        switch (inputType)
        {
            case ButtonInputType.PrimaryBtn:
                return CommonUsages.primaryButton;
            case ButtonInputType.SecondaryBtn:
                return CommonUsages.secondaryButton;
            default:
                return CommonUsages.primaryButton;
        }
    }

    public static InputFeatureUsage<Vector2> GetUsage(this AxisInputType inputType)
    {
        switch (inputType)
        {
            case AxisInputType.Primary2DAxis:
                return CommonUsages.primary2DAxis;
            case AxisInputType.Secondary2DAxis:
                return CommonUsages.secondary2DAxis;
            default:
                return CommonUsages.primary2DAxis;
        }
    }

    public static InputFeatureUsage<float> GetUsage(this TriggerInputType inputType)
    {
        switch (inputType)
        {
            case TriggerInputType.TriggerButton:
                return CommonUsages.trigger;
            case TriggerInputType.GripButton:
                return CommonUsages.grip;
            default:
                return CommonUsages.trigger;
        }
    }
}
