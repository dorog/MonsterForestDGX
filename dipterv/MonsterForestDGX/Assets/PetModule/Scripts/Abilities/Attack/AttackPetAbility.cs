using System;
using UnityEngine;

[Serializable]
public class AttackPetAbility : PetNextAction
{
    private IAttackable attackable;

    public override void Init(PetParameter _petParameter)
    {
        attackable = _petParameter.Attackable;
        base.Init(_petParameter);
    }

    public override void UpdateEffect()
    {
        if (isActivated && !inWait)
        {
            attackable.TakeDamageFromPet(effectAmount);
            SetUpNextEffect();
        }
    }

    protected override IPetParameter GetPetParameter(PetParameter petParameter)
    {
        return petParameter.Attackable;
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
