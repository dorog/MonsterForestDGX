
public class MockDataIO : DataIO
{
    public GameConfig gameConfig;
    public GameData LocalGameData = null;

    public override GameData Read()
    {
        return new GameData(gameConfig);
    }

    public override void Save(GameData gameData)
    {
        LocalGameData = gameData;
    }
}
