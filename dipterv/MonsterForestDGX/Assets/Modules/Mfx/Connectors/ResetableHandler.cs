using UnityEngine;

public class ResetableHandler : MonoBehaviour, IResetable
{
    public Resetable Resetable;
    private CooldownResetPetAbility cooldownReset;

    public void AddCooldownRef(CooldownResetPetAbility cooldownResetPetAbility)
    {
        if (cooldownReset != null)
        {
            cooldownReset.Deactivate();
            Resetable.UnsubscribeToReset(CallReset);
        }

        cooldownReset = cooldownResetPetAbility;

        if (cooldownResetPetAbility != null)
        {
            cooldownResetPetAbility.Activate();
            Resetable.SubscribeToReset(CallReset);
        }
    }

    private void CallReset()
    {
        cooldownReset.ResetCooldown();
    }

    public void ResetAction()
    {
        Resetable.ResetAction();
    }
}
