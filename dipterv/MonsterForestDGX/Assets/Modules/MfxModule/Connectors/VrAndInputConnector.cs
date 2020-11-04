using UnityEngine;

public class VrAndInputConnector : MonoBehaviour
{
    public ContinousMovementVR continousMovement;
    public ContinousRotationVR continousRotation;

    public KeyBindingManager keyBindingManager;

    public Player player;

    public DataManager dataManager;

    private void Start()
    {
        AxisInput movementAxisInput = keyBindingManager.continousMovementAxisInput;
        continousMovement.SetInput(movementAxisInput);
        player.Go += movementAxisInput.Activate;
        player.Stop += movementAxisInput.Deactivate;

        AxisInput rotationAxisInput = keyBindingManager.continousRotationAxisInput;
        continousRotation.SetInput(rotationAxisInput);
        player.Go += rotationAxisInput.Activate;
        player.Stop += rotationAxisInput.Deactivate;

        if (dataManager.IsTraningFinished())
        {
            player.EnableControlling();
        }
    }
}
