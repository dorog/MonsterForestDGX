using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XrRigControllable : VrControllable
{
    public XRRig rig;

    private readonly float additionalHeight = 0.01f;

    public override float GetCurrentHeight()
    {
        return rig.cameraInRigSpaceHeight + additionalHeight;
    }

    public override float GetHeadYawRotationY()
    {
        return rig.cameraGameObject.transform.eulerAngles.y;
    }

    public override Vector3 GetColliderCenter()
    {
        return rig.cameraGameObject.transform.position;
    }
}
