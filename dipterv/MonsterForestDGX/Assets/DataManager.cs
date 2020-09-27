using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : SingletonClass<DataManager>
{
    public readonly string fileName = "gameData.json";
    private static string deviceFileLocation;

    public GameConfig gameConfig;

    private static DataManager instance = null;

    private GameData gameData = null;

    public delegate void ExpChangedEvent(float exp);
    public ExpChangedEvent expChangedEvent;

    public delegate void SpellLevelChangedEvent(int id);
    public SpellLevelChangedEvent spellLevelChangedEvent;

    public void Awake()
    {
        deviceFileLocation = Application.persistentDataPath + "/" + fileName;

        Init(this);

        GetGameData();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        expChangedEvent(gameData.exp);
    }

    private void GetGameData()
    {
        if (File.Exists(deviceFileLocation))
        {
            //TODO: Check attack and defense data count?
            Read();
        }
        else
        {
            Create();
        }
    }

    private void Read()
    {
        gameData = JsonUtility.FromJson<GameData>(File.ReadAllText(deviceFileLocation));
    }

    private void Create()
    {
        gameData = new GameData(gameConfig);

        Save(gameData);
    }


    public void Save<T>(T data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(deviceFileLocation, jsonData);
    }

    public void SaveMonsterDeath(int id)
    {
        gameData.aliveMonsters[id] = false;

        Save(gameData);
    }

    public void SaveGateDeath(int id)
    {
        gameData.gatesState[id] = false;

        Save(gameData);
    }

    public void Won(List<ISpellPattern> spellPatterns, float exp)
    {
        gameData.exp = exp;
        for (int i = 0; i < spellPatterns.Count; i++)
        {
            gameData.basePatternSpellLevels[i] = spellPatterns[i].GetLevelValue();
        }

        Save(gameData);

        expChangedEvent(exp);
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

        Save(gameData);
    }

    public void SaveTeleportUnlock(int id)
    {
        gameData.teleports[id] = true;

        Save(gameData);
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
        return gameConfig.baseSpells.ToList();
    }

    public List<int> GetBasePatternLevels()
    {
        return gameData.basePatternSpellLevels.ToList();
    }

    public void LevelUpSpell(int id, int exp)
    {
        gameData.exp -= exp;
        gameData.basePatternSpellLevels[id]++;

        Save(gameData);

        expChangedEvent(gameData.exp);
        spellLevelChangedEvent(id);
    }

    public void SavePetDatas(PetData[] petDatas)
    {
        for(int i = 0; i < petDatas.Length; i++)
        {
            gameData.availablePets[i] = petDatas[i].available;
        }

        Save(gameData);
    }

    public int GetLastSelectedPet()
    {
        return gameData.lastSelectedPet;
    }

    public void SaveSelectedPet(int id)
    {
        gameData.lastSelectedPet = id;
        Save(gameData);
    }
}
