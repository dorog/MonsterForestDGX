using UnityEngine;
using UnityEngine.UI;

public class DamageBlockListener : MonoBehaviour
{
    public PlayerHealth player;
    public DamageBlock damageBlock;
    public Controller controller;

    public ShieldHandler shieldHandler;
    public Text angle;

    public bool startDefTurn = false;

    private void OnEnable()
    {
        if (startDefTurn)
        {
            controller.StartController();
        }
    }

    void Update()
    {
        angle.text = shieldHandler.GetAngle();
        if (player.damageBlock == damageBlock)
        {
            controller.StartController();
        }
    }
}
