using UnityEngine;

public class PetModuleConnector : MonoBehaviour
{
    private PetParameter petParameter = new PetParameter();

    [Header ("Managers")]
    public PetSystem petSystem;
    public PetManager petManager;
    public GameEvents gameEvents;

    [Header ("Components / Dependencies")]
    public PetSelectorComponent petSelectorComponent;
    public PetInitializerComponent petInitializerComponent;

    [Header("Dependencies (Not component)")]
    public MfxPetDataHandler petDataHandler;
    public MfxPetSelectorHandler petSelectorHandler;
    public MagicCircleHandler resetable;
    public Health healable;

    [Header("Event Assignments")]
    public PetTab petTab;

    public void Start()
    {
        AddDependencies();
        AssignToEvents();
        AssignToInputs();

        petSystem.InitializeComponents(petManager);
    }

    private void AddDependencies()
    {
        petManager.petDataHandler = petDataHandler;

        petSelectorComponent.petSelectorHandler = petSelectorHandler;

        petInitializerComponent.petSelectorComponent = petSelectorComponent;
        petInitializerComponent.petParameter = petParameter;

        petParameter.Resetable = resetable;
        petParameter.Healable = healable;
    }

    private void AssignToEvents()
    {
        gameEvents.BattleLobbyEnteredDelegateEvent += petTab.ShowTab;

        gameEvents.BattleStartDelegateEvent += petInitializerComponent.SummonPet;
        gameEvents.BattleEndDelegateEvent += petInitializerComponent.VanishPet;

        gameEvents.BattleLobbyEnteredDelegateEvent += SetPetInitializerProperties;
    }

    private void AssignToInputs()
    {

    }

    private void SetPetInitializerProperties()
    {
        petInitializerComponent.position = gameEvents.petPosition;
        petParameter.Attackable = gameEvents.enemyHealth;
    }
}
