using System;
using UnityEngine;

[Serializable]
public abstract class PetAbility : MonoBehaviour
{
    public abstract void Init(Player player);
    public abstract string GetAbilityName();
    public abstract Color GetAbilityNameColor();
    public abstract string GetAbilityDescription();
}
