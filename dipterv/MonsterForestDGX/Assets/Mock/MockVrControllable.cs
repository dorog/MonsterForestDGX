using UnityEngine;

public class MockVrControllable : VrControllable
{
    public float height = 1.72f;
    public Transform cameraTransform;

    private void Start()
    {
        cameraTransform.localPosition += Vector3.up * height;
    }

    public override Vector3 GetColliderCenter()
    {
        return transform.position + Vector3.up * height;
    }

    public override float GetCurrentHeight()
    {
        return height;
    }

    public override float GetHeadYawRotationY()
    {
        return transform.rotation.y;
    }
}
