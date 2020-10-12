
public class MockDataIO : DataIO
{
    public GameConfig gameConfig;
    public GameData LocalGameData = null;
    public ExperienceManager experienceManager;
    public DataManager dataManager;

    public void Start()
    {
        experienceManager.ExperienceIO = dataManager;

        experienceManager.Load();
    }

    public override GameData Read()
    {
        return new GameData(gameConfig);
    }

    public override void Save(GameData gameData)
    {
        LocalGameData = gameData;
    }
}
