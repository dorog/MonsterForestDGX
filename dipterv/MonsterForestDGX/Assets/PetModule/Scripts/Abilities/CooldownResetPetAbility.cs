using UnityEngine;
using System;

[Serializable]
public class CooldownResetPetAbility : PetAbility
{
    private Player player;
    [Range(0, 100)]
    public float resetChance = 50;

    private MagicCircleHandler magicCircleHandler;

    private ParticleSystem effect;

    public override void Init(Player _player)
    {
        player = _player;
        effect = _player.GetCooldownEffect();
        magicCircleHandler = player.GetMagicCircleHandler();
        magicCircleHandler.successCastSpellDelegateEvent += ResetCooldown;
    }

    private void ResetCooldown()
    {
        float random = UnityEngine.Random.Range(0, 100);
        if (random <= resetChance)
        {
            magicCircleHandler.ResetCooldown();
            effect.Play();
        }
    }

    public void DisableAbility()
    {
        magicCircleHandler.successCastSpellDelegateEvent -= ResetCooldown;
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
        return Color.blue;
    }
}
