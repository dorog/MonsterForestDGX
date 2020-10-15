using UnityEngine;

//TODO: Exp Collecting Test, Do I need it here?
public class PlayerUiConnector : MonoBehaviour
{
    public TextCooldownShower cooldownShower;
    public MagicCircleHandler magicCircleHandler;

    void Start()
    {
        magicCircleHandler.cooldownShower = cooldownShower;
    }
}
