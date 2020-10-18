using UnityEngine;

public class MonsterAttackChances : MonoBehaviour
{
    [Range(0, 100)]
    public float normalAttakChance = 70;
    [Range(0, 100)]
    public float hardAttackChance = 20;
    [Range(0, 100)]
    public float ultimateAttackChance = 10;
}
