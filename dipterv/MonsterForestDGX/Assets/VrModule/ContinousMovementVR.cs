using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ContinousMovementVR : MonoBehaviour
{
    public Player player;

    private AxisInput axisInput;
    private Vector2 inputAxis;
    public CharacterController character;

    public float speed = 3;
    public float gravity = -9.81f;
    private float fallingSpeed = 0f;

    public GameObject body;
    public XRRig rig;

    private readonly float additionalHeight = 0.01f;

    public LayerMask groundLayer;

    public KeyBindingManager keyBindingManager;

    private void Start()
    {
        axisInput = keyBindingManager.continousMovementAxisInput;

        axisInput.SubscibeToAxisChange(Move);

        player.Stopped += axisInput.Deactivate;
        player.Go += axisInput.Activate;
    }

    private void Move(Vector2 axis)
    {
        inputAxis += axis;
    } 

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
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
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }
}
