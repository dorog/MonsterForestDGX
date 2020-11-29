using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class GameData
{
    public int[] basePatternSpellLevels;
    public List<EnemyGroupData> enemys;
    public bool[] teleports;
    public bool[] availablePets;
    public float exp;
    public int lastLocation = -1;
    public int lastSelectedPet = -1;
    public bool traningFinished = false;
    public RewardState[] rewardStates;
    public GameData() { }

    public GameData(GameConfig gameConfig)
    {
        enemys = CreateEnemyList(gameConfig.enemies);
        teleports = gameConfig.teleports.ToArray();
        basePatternSpellLevels = gameConfig.baseSpells.Select(x => x.startLevel).ToArray();
        availablePets = gameConfig.pets.Select(x => x.Available).ToArray();
        exp = gameConfig.exp;
        lastSelectedPet = gameConfig.lastSelectedPet;
        lastLocation = gameConfig.lastLocation;
        traningFinished = gameConfig.traningFinished;
        rewardStates = gameConfig.battleRewards;
    }

    private List<EnemyGroupData> CreateEnemyList(EnemyConfig[] enemyConfigs)
    {
        List<EnemyGroupData> enemies = new List<EnemyGroupData>();

        foreach(var config in enemyConfigs)
        {
            enemies.Add(new EnemyGroupData() { group = config.group, enemyStates = new bool[config.enemies] });
        }

        return enemies;
    }
}
