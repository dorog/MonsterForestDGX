using UnityEngine;

public class PlayerUiConnector : MonoBehaviour
{
    public TextCooldownShower cooldownShower;
    public MagicCircleHandler magicCircleHandler;

    void Start()
    {
        magicCircleHandler.cooldownShower = cooldownShower;
    }
}
