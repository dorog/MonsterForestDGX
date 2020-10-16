using UnityEngine;

public abstract class DataIO : MonoBehaviour
{
    public abstract GameData Read();
    public abstract void Save(GameData gameData);
}
