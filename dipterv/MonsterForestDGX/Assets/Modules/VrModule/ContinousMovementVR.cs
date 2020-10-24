using UnityEngine;

public class ContinousMovementVR : MonoBehaviour
{
    private AxisInput axisInput;
    private Vector2 inputAxis;
    public CharacterController character;

    public float speed = 3;
    public float gravity = -9.81f;
    private float fallingSpeed = 0f;

    public VrControllable vrControllable;

    public LayerMask groundLayer;

    public void SetInput(AxisInput _axisInput)
    {
        axisInput = _axisInput;

        axisInput.SubscibeToAxisChange(Move);
    }

    private void Move(Vector2 axis)
    {
        inputAxis += axis;
    } 

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, vrControllable.GetHeadYawRotationY(), 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        Gravity();

        inputAxis = Vector2.zero;
    }

    private void Gravity()
    {
        if (CheckIfGrounded())
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    private bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }

    private void CapsuleFollowHeadset()
    {
        character.height = vrControllable.GetCurrentHeight();
        Vector3 capsuleCenter = transform.InverseTransformPoint(vrControllable.GetColliderCenter());
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
