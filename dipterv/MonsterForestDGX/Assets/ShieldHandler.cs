﻿using UnityEngine;

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

    public void Start()
    {
        shieldActivateButton = KeyBindingManager.GetInstance().shieldHandlerButtonInput;

        shieldActivateButton.SubscribeToPressed(Def);

        GameEvents gameEvents = GameEvents.GetInstance();
        gameEvents.BattleStartDelegateEvent += Fighting;
        gameEvents.BattleEndDelegateEvent += Exploring;
    }

    private void Fighting()
    {
        battleManager.MonsterTurnStartDelegateEvent += shieldActivateButton.Activate;
        battleManager.PlayerTurnStartDelegateEvent += shieldActivateButton.Deactivate;
    }

    private void Exploring()
    {
        battleManager.MonsterTurnStartDelegateEvent -= shieldActivateButton.Activate;
        battleManager.PlayerTurnStartDelegateEvent -= shieldActivateButton.Deactivate;

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
