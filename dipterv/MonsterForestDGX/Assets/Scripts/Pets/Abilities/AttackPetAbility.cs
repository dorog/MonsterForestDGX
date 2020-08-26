using System;

[Serializable]
public class AttackPetAbility : PetNextAction
{
    private Player player;
    private MonsterHealth monsterHealth;

    public override void Init(Player _player)
    {
        player = _player;
        monsterHealth = player.battleManager.monster.GetHealth() as MonsterHealth;
        base.Init(player);
    }

    public override void UpdateEffect()
    {
        if (player.CanAttack() && !inWait)
        {
            monsterHealth.TakeDamageFromPet(effectAmount, ElementType.TrueDamage);
            SetUpNextEffect();
        }
    }
}
