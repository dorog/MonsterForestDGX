using UnityEngine;

public class VrAndInputConnector : MonoBehaviour
{
    public ContinousMovementVR continousMovement;
    public ContinousRotationVR continousRotation;

    public KeyBindingManager keyBindingManager;

    public MovingHandler movingHandler;

    public DataManager dataManager;

    private void Start()
    {
        AxisInput movementAxisInput = keyBindingManager.continousMovementAxisInput;
        continousMovement.SetInput(movementAxisInput);
        movingHandler.Go += movementAxisInput.Activate;
        movingHandler.Stop += movementAxisInput.Deactivate;

        AxisInput rotationAxisInput = keyBindingManager.continousRotationAxisInput;
        continousRotation.SetInput(rotationAxisInput);
        movingHandler.Go += rotationAxisInput.Activate;
        movingHandler.Stop += rotationAxisInput.Deactivate;

        if (dataManager.IsTraningFinished())
        {
            movingHandler.EnableMovement();
        }
    }
}
