using System;
using UnityEngine;

[Serializable]
public abstract class PetAbility : MonoBehaviour
{
    protected bool isActivated = false;

    public void Activate()
    {
        isActivated = true;
    }
    public void Deactivate()
    {
        isActivated = false;
    }

    public abstract void Init(PetParameter _petParameter);
    public abstract void Destroy();
    public abstract string GetAbilityName();
    public abstract Color GetAbilityNameColor();
    public abstract string GetAbilityDescription();
}
