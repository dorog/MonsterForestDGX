using UnityEngine;

[CreateAssetMenu(fileName = "new GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public bool traningFinished = false;

    [Header("Player Settings")]
    public float exp;

    [Header("Enemy Settings")]
    public EnemyConfig[] enemies;
    public RewardState[] battleRewards;

    [Header("Teleports Settings")]
    public bool[] teleports;
    public int lastLocation = -1;

    [Header("Attack Spells Settings")]
    public SpellConfig[] baseSpells;

    [Header("Pet Settings")]
    public PetConfig[] pets;
    public int lastSelectedPet = -1;
}
