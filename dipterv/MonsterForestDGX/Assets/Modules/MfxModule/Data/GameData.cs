using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameData
{
    public int[] basePatternSpellLevels;
    public bool[] enemys;
    public bool[] teleports;
    public bool[] availablePets;
    public float exp;
    public int lastLocation = -1;
    public int lastSelectedPet = -1;
    public bool traningFinished = false;

    public GameData() { }

    public GameData(GameConfig gameConfig)
    {
        enemys = gameConfig.enemies == 0 ? null : CreateArrayWithDefaultValue(gameConfig.enemies, true);
        teleports = gameConfig.teleports.ToArray();
        basePatternSpellLevels = gameConfig.baseSpells.Select(x => x.startLevel).ToArray();
        availablePets = gameConfig.pets.Select(x => x.Available).ToArray();
        exp = gameConfig.exp;
        lastSelectedPet = gameConfig.lastSelectedPet;
        lastLocation = gameConfig.lastLocation;
        traningFinished = gameConfig.traningFinished;
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
