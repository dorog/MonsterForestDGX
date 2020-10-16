using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Transform hand;

    public float continuesDamageShieldMinAngle = 0;
    public float continuesDamageShieldMaxAngle = 25;
    public TimeDamageBlock continouesTimeDamageBlock;

    public float simpleDamageShieldMinAngle = 65;
    public float simpleDamageShieldMaxAngle = 90;
    public TimeDamageBlock simpleTimeDamageBlock;

    private IPressed shieldActivateButton;

    public BattleManager battleManager;
    public GameEvents gameEvents;
    public KeyBindingManager keyBindingManager;

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
        float angle = Vector3.Angle(hand.forward, Vector3.up);

        if (angle <= simpleDamageShieldMaxAngle && angle >= simpleDamageShieldMinAngle)
        {
            playerHealth.timeDamageBlock = simpleTimeDamageBlock;
            playerHealth.SetDamageBlock();
        }
        else if (angle <= continuesDamageShieldMaxAngle && angle >= continuesDamageShieldMinAngle)
        {
            playerHealth.timeDamageBlock = continouesTimeDamageBlock;
            playerHealth.SetDamageBlock();
        }
    }
}
