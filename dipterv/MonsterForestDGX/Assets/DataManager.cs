using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : SingletonClass<DataManager>
{
    public readonly string fileName = "gameData.json";
    private static string deviceFileLocation;

    public GameConfig gameConfig;

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

    public void Won(float exp)
    {
        gameData.exp = exp;

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

    public float LevelUpSpell(int id, float price)
    {
        gameData.exp -= price;
        gameData.basePatternSpellLevels[id]++;

        Save(gameData);

        return gameData.exp;
        //expChangedEvent(gameData.exp);
        //spellLevelChangedEvent(id);
    }

    public void DecreasePrice(int quantity)
    {
        gameData.exp -= quantity;
        Save(gameData);
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

    public float GetExp()
    {
        return gameData.exp;
    }
}
