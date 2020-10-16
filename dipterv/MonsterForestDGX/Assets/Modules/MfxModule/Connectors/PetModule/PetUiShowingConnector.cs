
public class PetUiShowingConnector : AbstractConnector
{
    public PetUiShowerComponent petUiShowerComponent;
    public PetManager petManager;

    public override void Setup()
    {
        petUiShowerComponent.AddPetManager(petManager);
    }

    public override void Load() { }
}
