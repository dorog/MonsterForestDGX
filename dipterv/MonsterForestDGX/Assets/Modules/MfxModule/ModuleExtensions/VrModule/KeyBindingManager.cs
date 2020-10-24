using UnityEngine;

public class KeyBindingManager : MonoBehaviour
{
    public AxisInput continousMovementAxisInput;
    public AxisInput continousRotationAxisInput;

    public AbstractPressed shieldHandlerButtonInput;

    public AbstractPressed magicCircleInput;
    
    public TriggerInput paintingTrigger;

    public AbstractPressed petCollectButton;

    public AbstractPressed shopCollectButton;

    public AbstractPressed[] drawHelperInputs;
    public SwitchInput drawHelperInput;
}
