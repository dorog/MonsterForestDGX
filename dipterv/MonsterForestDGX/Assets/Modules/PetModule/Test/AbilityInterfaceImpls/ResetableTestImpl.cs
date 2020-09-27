using System;
using UnityEngine;

public class ResetableTestImpl : PetParameterTestImpl, IResetable
{
    private CooldownResetPetAbility _cooldownResetPetAbility;

    public void CastWhatever()
    {
        _cooldownResetPetAbility.ResetCooldown();
    }

    public void AddCooldownRef(CooldownResetPetAbility cooldownResetPetAbility)
    {
        _cooldownResetPetAbility = cooldownResetPetAbility;
    }

    public void ResetAction()
    {
        Debug.Log(nameof(HealableTestImpl) + ": Reseted");
    }
}
