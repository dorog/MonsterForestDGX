
public class PetManagerConnector : AbstractConnector
{
    public PetManager petManager;
    public MfxPetDataHandler petDataHandler;

    public override void Setup() 
    {
        petManager.petDataHandler = petDataHandler;
    }

    public override void Load()
    {
        petManager.LoadData();
    }
}
