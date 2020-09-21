using System;
using UnityEngine;

[Serializable]
public class AttackPetAbility : PetNextAction
{
    private Player player;
    private MonsterHealth monsterHealth;

    private bool attackTurn = false;

    public override string GetAbilityDescription()
    {
        return "In your turn it deals " + effectAmount + " damage to your opponent" + GetTimeRangeMessage();
    }

    public override string GetAbilityName()
    {
        return "Fire breathe";
    }

    public override Color GetAbilityNameColor()
    {
        return Color.red;
    }

    public override void Init(Player _player)
    {
        player = _player;
        monsterHealth = player.battleManager.monster.GetHealth() as MonsterHealth;
        player.battleManager.PlayerTurnStartDelegateEvent += AttackTurn;
        player.battleManager.PlayerTurnEndDelegateEvent += DefTurn;
        base.Init(player);
    }

    public override void UpdateEffect()
    {
        if (attackTurn && !inWait)
        {
            monsterHealth.TakeDamageFromPet(effectAmount, ElementType.TrueDamage);
            SetUpNextEffect();
        }
    }

    private void AttackTurn()
    {
        attackTurn = true;
    }

    private void DefTurn()
    {
        attackTurn = false;
    }
}
