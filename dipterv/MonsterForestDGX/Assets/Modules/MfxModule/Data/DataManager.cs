﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour, IExperienceIO
{
    //TODO: Remove somehow
    public GameConfig gameConfig;

    private GameData gameData = null;

    public delegate void SpellLevelChangedEvent(int id);
    public SpellLevelChangedEvent spellLevelChangedEvent;

    public DataIO dataIO;

    public void Awake()
    {
        gameData = dataIO.Read();
    }

    public void SaveEnemyDeath(string group, int id)
    {
        if(id < 0)
        {
            return;
        }

        EnemyGroupData enemyGroupData = gameData.enemys.Find(x => x.group == group);
        if (enemyGroupData != null) 
        {
            enemyGroupData.enemyStates[id] = true;
            dataIO.Save(gameData);
        }
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
                pet = gameConfig.pets[i].Pet
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

    public void SaveTeleportUnlock(int id, bool state)
    {
        gameData.teleports[id] = state;

        dataIO.Save(gameData);
    }

    public List<EnemyGroupData> GetEnemies()
    {
        return gameData.enemys;
    }

    public bool[] GetTeleportsState()
    {
        return gameData.teleports;
    }

    public List<PatternSpellData> GetBasePatterns()
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

    //Traning

    public void TutorialFinished()
    {
        gameData.traningFinished = true;
        dataIO.Save(gameData);
    }

    public bool IsTraningFinished()
    {
        return gameData.traningFinished;
    }

    //Reward

    public RewardState[] GetRewardStates()
    {
        return gameData.rewardStates;
    }

    public void RewardStateChanged(int id, RewardState rewardState) 
    {
        gameData.rewardStates[id] = rewardState;
        dataIO.Save(gameData);
    }
}
