using UnityEngine;
using System;

[Serializable]
public class CooldownResetPetAbility : PetAbility
{
    private IResetable resetable;
    [Range(0, 100)]
    public float resetChance = 50;

    public void ResetCooldown()
    {
        if (isActivated)
        {
            float random = UnityEngine.Random.Range(0, 101);
            if (random <= resetChance)
            {
                resetable.ResetAction();
            }
        }
    }

    public override void Init(PetParameter _petParameter)
    {
        resetable = _petParameter.Resetable;
        resetable?.AddCooldownRef(this);
    }

    public override void Destroy()
    {
        resetable?.AddCooldownRef(null);
    }

    public override string GetAbilityName()
    {
        return "Time travel";
    }

    public override string GetAbilityDescription()
    {
        return "After casting a spell, there is a " + resetChance + "% chance to reset the time for the next spell.";
    }

    public override Color GetAbilityNameColor()
    {
        return Color.cyan;
    }
}
