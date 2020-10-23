using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData
{
    public int[] basePatternSpellLevels;
    public bool[] aliveMonsters;
    public bool[] gatesState;
    public bool[] teleports;
    public bool[] availablePets;
    public float exp;
    public int lastLocation = -1;
    public int lastSelectedPet = -1;

    public GameData() { }

    public GameData(GameConfig gameConfig)
    {
        aliveMonsters = gameConfig.aliveMonsters == 0 ? null : CreateArrayWithDefaultValue(gameConfig.aliveMonsters, true);
        gatesState = gameConfig.gatesState == 0 ? null : CreateArrayWithDefaultValue(gameConfig.gatesState, true);
        teleports = gameConfig.teleports.ToArray();
        basePatternSpellLevels = gameConfig.baseSpells.Select(x => x.startLevel).ToArray();
        availablePets = gameConfig.pets.Select(x => x.Available).ToArray();
        exp = gameConfig.exp;
        lastSelectedPet = gameConfig.lastSelectedPet;
        lastLocation = gameConfig.lastLocation;
    }

    private T[] CreateArrayWithDefaultValue<T>(int count, T value)
    {
        if (count == 0)
        {
            Debug.LogWarning(nameof(CreateArrayWithDefaultValue) + " Warning!");
            return new T[1];
        }
        else
        {
            T[] array = new T[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = value;
            }

            return array;
        }
    }
}
