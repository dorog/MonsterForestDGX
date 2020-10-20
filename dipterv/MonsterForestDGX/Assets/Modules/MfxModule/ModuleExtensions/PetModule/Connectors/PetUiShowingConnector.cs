
public class PetUiShowingConnector : AbstractConnector
{
    public PetUiShowerComponent petUiShowerComponent;
    public MfxPetManager petManager;

    public override void Setup()
    {
        petUiShowerComponent.AddPetManager(petManager);
    }

    public override void Load() { }
}
