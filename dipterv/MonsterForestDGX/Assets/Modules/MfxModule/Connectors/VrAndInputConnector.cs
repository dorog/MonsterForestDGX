using UnityEngine;

public class VrAndInputConnector : MonoBehaviour
{
    public ContinousMovementVR continousMovement;
    public ContinousRotationVR continousRotation;

    public KeyBindingManager keyBindingManager;

    public Player player;

    private void Start()
    {
        AxisInput movementAxisInput = keyBindingManager.continousMovementAxisInput;
        continousMovement.SetInput(movementAxisInput);
        player.Go += movementAxisInput.Activate;
        player.Stopped += movementAxisInput.Deactivate;

        AxisInput rotationAxisInput = keyBindingManager.continousRotationAxisInput;
        continousRotation.SetInput(rotationAxisInput);
        player.Go += rotationAxisInput.Activate;
        player.Stopped += rotationAxisInput.Deactivate;

        player.EnableControlling();
    }
}
