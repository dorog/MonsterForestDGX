using System;
using UnityEngine;

[Serializable]
public abstract class PetAbility : MonoBehaviour
{
    private IPetParameter petParameter;
    protected bool isActivated = false;

    public virtual void Init(PetParameter _petParameter)
    {
        petParameter = GetPetParameter(_petParameter);
        petParameter?.SubscribeToEvents(Activate, Deactivate);
    }

    public void Destroy()
    {
        petParameter?.UnsubscribeToEvents(Activate, Deactivate);
    }
    protected void Activate()
    {
        isActivated = true;
    }
    protected void Deactivate()
    {
        isActivated = false;
    }

    protected abstract IPetParameter GetPetParameter(PetParameter petParameter);
    public abstract string GetAbilityName();
    public abstract Color GetAbilityNameColor();
    public abstract string GetAbilityDescription();
}
