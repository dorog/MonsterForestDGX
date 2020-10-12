using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour, IExperienceIO
{
    public GameConfig gameConfig;

    private GameData gameData = null;

    public delegate void SpellLevelChangedEvent(int id);
    public SpellLevelChangedEvent spellLevelChangedEvent;

    public DataIO dataIO;

    public void Awake()
    {
        gameData = dataIO.Read();
    }

    public void SaveMonsterDeath(int id)
    {
        gameData.aliveMonsters[id] = false;

        dataIO.Save(gameData);
    }

    public void SaveGateDeath(int id)
    {
        gameData.gatesState[id] = false;

        dataIO.Save(gameData);
    }

    public void Won(float exp)
    {
        gameData.exp = exp;

        dataIO.Save(gameData);
    }

    public PetData[] GetPetDatas()
    {
        List<PetData> petDatas = new List<PetData>();
        for(int i = 0; i < gameConfig.pets.Length; i++)
        {
            petDatas.Add(new PetData() 
            { 
                available = gameData.availablePets[i],
                pet = gameConfig.pets[i]
            });
        }

        return petDatas.ToArray();
    }

    public int GetLastLocation()
    {
        return gameData.lastLocation;
    }

    public void SavePortLocation(int id)
    {
        gameData.lastLocation = id;

        dataIO.Save(gameData);
    }

    public void SaveTeleportUnlock(int id)
    {
        gameData.teleports[id] = true;

        dataIO.Save(gameData);
    }

    public bool[] GetAliveMonsters()
    {
        return gameData.aliveMonsters;
    }

    public bool[] GetGatesState()
    {
        return gameData.gatesState;
    }

    public bool[] GetTeleportsState()
    {
        return gameData.teleports;
    }

    public List<BasePatternSpell> GetBasePatterns()
    {
        return gameConfig.baseSpells.Select(x => x.basePatternSpell).ToList();
    }

    public List<int> GetBasePatternLevels()
    {
        return gameData.basePatternSpellLevels.ToList();
    }

    public void LevelUpSpell(int id)
    {
        gameData.basePatternSpellLevels[id]++;

        dataIO.Save(gameData);
    }

    public void SavePetDatas(PetData[] petDatas)
    {
        for(int i = 0; i < petDatas.Length; i++)
        {
            gameData.availablePets[i] = petDatas[i].available;
        }

        dataIO.Save(gameData);
    }

    public int GetLastSelectedPet()
    {
        return gameData.lastSelectedPet;
    }

    public void SaveSelectedPet(int id)
    {
        gameData.lastSelectedPet = id;
        dataIO.Save(gameData);
    }

    //Experience

    public void SaveExp(float exp)
    {
        gameData.exp = exp;
        dataIO.Save(gameData);
    }

    public float LoadExp()
    {
        return gameData.exp;
    }
}
