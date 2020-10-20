
public class ExperienceSaveConnector : AbstractConnector
{
    public ExperienceManager experienceManager;
    public DataManager dataManager;

    public override void Setup()
    {
        experienceManager.ExperienceIO = dataManager;
    }

    public override void Load()
    {
        experienceManager.Load();
    }
}
