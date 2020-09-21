using System;
using UnityEngine;

[Serializable]
public class HealPetAbility : PetNextAction
{
    private PlayerHealth health;

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

    public override void Init(Player player)
    {
        health = player.GetComponent<PlayerHealth>();
        base.Init(player);
    }

    public override void UpdateEffect()
    {
        if (!inWait && (health.maxHp > health.currentHp))
        {
            health.Heal(effectAmount);
            SetUpNextEffect();
        }
    }
}
