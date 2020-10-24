using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private IPressed shieldActivateButton;

    public BattleManager battleManager;
    public GameEvents gameEvents;
    public KeyBindingManager keyBindingManager;

    public PlayerHealth playerHealth;

    public Transform hand;

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

        float angle = Vector3.Angle(hand.forward, Vector3.up);

        foreach(var shield in playerShields)
        {
            if (angle <= shield.maxAngle && angle >= shield.minAngle)
            {
                playerHealth.damageBlock = shield.damageBlock;
                playerHealth.SetDamageBlock();

                break;
            }
        }
    }
}
