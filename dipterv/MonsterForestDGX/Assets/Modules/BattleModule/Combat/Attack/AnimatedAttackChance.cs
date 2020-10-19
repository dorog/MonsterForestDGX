using UnityEngine;

public class AnimatedAttackChance : MonoBehaviour
{
    [Range(0, 100)]
    public int chance = 33;
    public AnimatedAttackData[] monsterAttacks;
}
