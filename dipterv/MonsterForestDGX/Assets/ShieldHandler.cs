using UnityEngine;
using UnityEngine.UI;

public class ShieldHandler : MonoBehaviour
{
    public Player player;
    public PlayerHealth playerHealth;

    public Text feedback;

    public Transform hand;

    public float continuesDamageShieldMinAngle = 0;
    public float continuesDamageShieldMaxAngle = 25;
    public TimeDamageBlock continouesTimeDamageBlock;

    public float simpleDamageShieldMinAngle = 65;
    public float simpleDamageShieldMaxAngle = 90;
    public TimeDamageBlock simpleTimeDamageBlock;

    private IPressed shieldActivateButton;

    private void Start()
    {
        shieldActivateButton = KeyBindingManager.GetInstance().shieldHandlerButtonInput;
    }

    void Update()
    {
        if(player.InBattle && !player.CanAttack())
        {
            float angle = Vector3.Angle(hand.forward, Vector3.up);

            if (shieldActivateButton.IsPressing())
            {
                if(angle <= simpleDamageShieldMaxAngle && angle >= simpleDamageShieldMinAngle)
                {
                    playerHealth.timeDamageBlock = simpleTimeDamageBlock;
                    playerHealth.SetDamageBlock();
                }
                else if(angle <= continuesDamageShieldMaxAngle && angle >= continuesDamageShieldMinAngle)
                {
                    playerHealth.timeDamageBlock = continouesTimeDamageBlock;
                    playerHealth.SetDamageBlock();
                }
            }
        }
    }
}
