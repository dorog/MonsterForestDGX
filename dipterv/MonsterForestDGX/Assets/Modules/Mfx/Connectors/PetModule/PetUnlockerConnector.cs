
public class PetUnlockerConnector : AbstractConnector
{
    public PetUnlockerComponent petUnlockerComponent;
    public PetManager petManager;

    public override void Setup()
    {
        petUnlockerComponent.AddPetManager(petManager);
    }

    public override void Load() { }
}
