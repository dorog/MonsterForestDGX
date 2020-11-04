using System.IO;
using UnityEngine;

public class FileDataIO : DataIO
{
    public readonly string fileName = "gameData.json";
    private static string deviceFileLocation;
    public GameConfig gameConfig;

    public void Awake()
    {
        deviceFileLocation = Application.persistentDataPath + "/" + fileName;
    }

    public override GameData Read()
    {
        if (File.Exists(deviceFileLocation))
        {
            //TODO: Check attack and defense data count?
            return JsonUtility.FromJson<GameData>(File.ReadAllText(deviceFileLocation));
        }
        else
        {
            return Create();
        }
    }

    public bool HasFile()
    {
        return File.Exists(deviceFileLocation);
    }

    public GameData Create()
    {
        GameData gameData = new GameData(gameConfig);

        Save(gameData);

        return gameData;
    }

    public override void Save(GameData gameData)
    {
        Save(gameData);
    }

    private void Save<T>(T data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(deviceFileLocation, jsonData);
    }
}
