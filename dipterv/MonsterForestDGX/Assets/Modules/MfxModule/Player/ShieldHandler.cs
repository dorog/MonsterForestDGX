using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private IPressed shieldActivateButton;

    public BattleManager battleManager;
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
        battleManager.RedFighterTurnStartDelegateEvent += shieldActivateButton.Activate;
        battleManager.BlueFighterTurnStartDelegateEvent += shieldActivateButton.Deactivate;
    }

    private void Exploring()
    {
        battleManager.RedFighterTurnStartDelegateEvent -= shieldActivateButton.Activate;
        battleManager.BlueFighterTurnStartDelegateEvent -= shieldActivateButton.Deactivate;

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
        float angle = Vector3.SignedAngle(handVector, Vector3.forward, Vector3.right);

        foreach(var shield in playerShields)
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

    public void BlockDown()
    {
        playerHealth.damageBlock.UnsubscribeToBlockDown(BlockDown);
        playerHealth.damageBlock = null;
    }
}
