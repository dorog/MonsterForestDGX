using UnityEngine;

public abstract class VrControllable : MonoBehaviour
{
    public abstract float GetCurrentHeight();
    public abstract float GetHeadYawRotationY();
    public abstract Vector3 GetColliderCenter();
}
