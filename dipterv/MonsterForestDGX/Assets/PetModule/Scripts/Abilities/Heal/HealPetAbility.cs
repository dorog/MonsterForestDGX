using System;
using UnityEngine;

[Serializable]
public class HealPetAbility : PetNextAction
{
    private IHealable healable;

    public override string GetAbilityDescription()
    {
        return "If the player's HP is not full, it increases the player's HP by " + effectAmount + GetTimeRangeMessage();
    }

    public override string GetAbilityName()
    {
        return "Angel hug";
    }

    public override Color GetAbilityNameColor()
    {
        return Color.green;
    }

    public override void Init(PetParameter _petParameter)
    {
        healable = _petParameter.Healable;
        base.Init(_petParameter);
    }

    public override void UpdateEffect()
    {
        if (isActivated && !inWait && !healable.IsFull())
        {
            healable.Heal(effectAmount);
            SetUpNextEffect();
        }
    }

    protected override IPetParameter GetPetParameter(PetParameter petParameter)
    {
        return petParameter.Healable;
    }
}
