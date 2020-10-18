using UnityEngine;

public class MonsterAttackChance : MonoBehaviour
{
    [Range(0, 100)]
    public int chance = 33;
    public MonsterAttackData[] monsterAttacks;
}
