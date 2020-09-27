using System;
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
        aliveMonsters = CreateArrayWithDefaultValue(gameConfig.aliveMonsters, true);
        gatesState = CreateArrayWithDefaultValue(gameConfig.gatesState, true);
        teleports = CreateArrayWithDefaultValue(gameConfig.teleports, false);
        basePatternSpellLevels = CreateArrayWithDefaultValue(gameConfig.baseSpells.Length, 0);
        availablePets = CreateArrayWithDefaultValue(gameConfig.pets.Length, false);
        exp = gameConfig.exp;
        lastSelectedPet = gameConfig.lastSelectedPet;
    }

    public T[] CreateArrayWithDefaultValue<T>(int count, T value)
    {
        if (count == 0)
        {
            Debug.LogError(nameof(CreateArrayWithDefaultValue) + " Error!");
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
