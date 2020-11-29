using UnityEngine;
using UnityEngine.UI;

public class ShieldHandler : MonoBehaviour
{
    public Text ui;

    private IPressed shieldActivateButton;

    public RoundHandler roundHandler;
    public GameEvents gameEvents;
    public KeyBindingManager keyBindingManager;

    public PlayerHealth playerHealth;

    public Transform hand;
    public Transform body;

    [Header("Shields")]
    public PlayerShield[] playerShields;

    public void Start()
    {
        shieldActivateButton = keyBindingManager.shieldHandlerButtonInput;

        shieldActivateButton.SubscribeToPressed(Def);

        gameEvents.BattleStartDelegateEvent += Fighting;
        gameEvents.BattleEndDelegateEvent += Exploring;
    }

    private void Fighting()
    {
        roundHandler.SubscribeToEndTurn(shieldActivateButton.Activate);
        roundHandler.SubscribeToStartTurn(shieldActivateButton.Deactivate);
    }

    private void Exploring()
    {
        roundHandler.SubscribeToEndTurn(shieldActivateButton.Activate);
        roundHandler.SubscribeToStartTurn(shieldActivateButton.Deactivate);

        shieldActivateButton.Deactivate();
    }

    private void Def()
    {
        if(playerHealth.damageBlock != null)
        {
            return;
        }

        Debug.Log("Body: if not rotate with controller it can be wrong!");
        Vector3 handVector = Vector3.ProjectOnPlane(hand.forward, body.forward);
        float angle = Vector3.SignedAngle(handVector, body.right, body.forward * -1);

        ui.text = angle.ToString();

        foreach (var shield in playerShields)
        {
            if (angle <= shield.maxAngle && angle >= shield.minAngle)
            {
                playerHealth.damageBlock = shield.damageBlock;
                playerHealth.SetDamageBlock();

                shield.damageBlock.SubscribeToBlockDown(BlockDown);

                break;
            }
        }
    }

    public string GetAngle()
    {
        Vector3 handVector = Vector3.ProjectOnPlane(hand.forward, body.forward);
        float angle = Vector3.SignedAngle(handVector, body.right, body.forward * -1);

        return angle.ToString("0");
    }

    public void BlockDown()
    {
        playerHealth.damageBlock.UnsubscribeToBlockDown(BlockDown);
        playerHealth.damageBlock = null;
    }
}
