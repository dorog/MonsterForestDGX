using System;
using UnityEngine;

[Serializable]
public class AttackPetAbility : PetNextAction
{
    private IAttackable attackable;

    public override void Init(PetParameter _petParameter)
    {
        attackable = _petParameter.Attackable;
        attackable?.SubscribeToAttackEvents(Activate, Deactivate);
    }

    public override void Destroy()
    {
        attackable?.UnsubscribeFromAttackEvents(Activate, Deactivate);
    }

    public override void UpdateEffect()
    {
        if (isActivated && !inWait)
        {
            attackable.TakeDamageFromPet(effectAmount);
            SetUpNextEffect();
        }
    }

    public override string GetAbilityDescription()
    {
        return "In your turn it deals " + effectAmount + " damage to your opponent" + GetTimeRangeMessage();
    }

    public override string GetAbilityName()
    {
        return "Fire breathe";
    }

    public override Color GetAbilityNameColor()
    {
        return Color.red;
    }
}
