
public class KeyBindingManager : SingletonClass<KeyBindingManager>
{
    public AxisInput continousMovementAxisInput;
    public AxisInput continousRotationAxisInput;

    public ButtonInput shieldHandlerButtonInput;

    public ButtonInput[] magicCircleInputs;
    public MultiSwitchInput magicCircleInput;
    
    public TriggerInput paintingTrigger;

    public ButtonInput petCollectButton;

    public ButtonInput shopCollectButton;

    public ButtonInput[] drawHelperInputs;
    public MultiSwitchInput drawHelperInput;

    private void Awake()
    {
        Init(this);
    }
}
