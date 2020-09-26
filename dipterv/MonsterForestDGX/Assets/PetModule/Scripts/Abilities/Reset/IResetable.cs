
using System;

public interface IResetable : IPetParameter
{
    void ResetAction();
    void AddCooldownRef(CooldownResetPetAbility cooldownResetPetAbility);
}
