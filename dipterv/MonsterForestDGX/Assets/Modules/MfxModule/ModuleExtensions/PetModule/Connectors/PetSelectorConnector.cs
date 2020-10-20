
public class PetSelectorConnector : AbstractConnector
{
    public PetSelectorComponent petSelectorComponent;
    public MfxPetManager petManager;
    public MfxPetSelectorHandler petSelectorHandler;
    public GameEvents gameEvents;

    public override void Setup()
    {
        petSelectorComponent.AddPetManager(petManager);
        petSelectorComponent.AddSelectorHandler(petSelectorHandler);

        gameEvents.BattleLobbyEnteredDelegateEvent += PetTabShowCheck;
        gameEvents.BattleStartDelegateEvent += petSelectorComponent.HideSelectorTab;
    }

    public override void Load() { }

    private void PetTabShowCheck()
    {
        if (gameEvents.petEnable)
        {
            petSelectorComponent.ShowSelectorTab();
        }
    }
}
