
public class KeyBindingManager : SingletonClass<KeyBindingManager>
{
    public AxisInput continousMovementAxisInput;
    public AxisInput continousRotationAxisInput;

    public ButtonInput shieldHandlerButtonInput;

    public ButtonInput magicCircleInput;
    
    public TriggerInput paintingTrigger;

    public ButtonInput petCollectButton;

    public ButtonInput shopCollectButton;

    public ButtonInput[] drawHelperInputs;
    public SwitchInput drawHelperInput;

    private void Awake()
    {
        Init(this);
    }
}
