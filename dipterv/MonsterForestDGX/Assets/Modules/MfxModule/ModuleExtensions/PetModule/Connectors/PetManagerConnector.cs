
public class PetManagerConnector : AbstractConnector
{
    public MfxPetManager petManager;
    public MfxPetDataHandler petDataHandler;

    public override void Setup() 
    {
        petManager.AddPetDataHandler(petDataHandler);
    }

    public override void Load()
    {
        petManager.LoadData();
    }
}
