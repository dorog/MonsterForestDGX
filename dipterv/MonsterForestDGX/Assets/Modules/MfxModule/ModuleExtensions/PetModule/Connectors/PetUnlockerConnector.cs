
public class PetUnlockerConnector : AbstractConnector
{
    public PetUnlockerComponent petUnlockerComponent;
    public MfxPetManager petManager;

    public override void Setup()
    {
        petUnlockerComponent.AddPetManager(petManager);
    }

    public override void Load() { }
}
