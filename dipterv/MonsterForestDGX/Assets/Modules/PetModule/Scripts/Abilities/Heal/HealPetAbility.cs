using System;
using UnityEngine;

[Serializable]
public class HealPetAbility : PetNextAction
{
    private IHealable healable;

    public override void Init(GameObject owner)
    {
        healable = owner.GetComponent<IHealable>();
        healable?.SubscribeToHealEvents(Activate, Deactivate);
    }

    public override void Destroy()
    {
        healable?.UnsubscribeFromHealEvents(Activate, Deactivate);
    }

    public override void UpdateEffect()
    {
        if (isActivated && !inWait && !healable.IsFull())
        {
            healable.Heal(effectAmount);
            SetUpNextEffect();
        }
    }

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
}
