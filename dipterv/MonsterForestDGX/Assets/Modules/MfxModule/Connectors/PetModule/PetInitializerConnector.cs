using UnityEngine;

public class PetInitializerConnector : AbstractConnector
{
    public PetInitializerComponent petInitializerComponent;

    public GameEvents gameEvents;
    
    public PetManager petManager;

    [Tooltip ("Must have an IPetSelector component!")]
    public GameObject petSelectorGO;

    private GameObject petOwner;

    public override void Setup()
    {
        IPetSelector petSelector = petSelectorGO.GetComponent<IPetSelector>();

        petInitializerComponent.petSelectorComponent = petSelector;
        petInitializerComponent.petOwner = petOwner;

        gameEvents.BattleStartDelegateEvent += PetSummonEnabledCheck;
        gameEvents.BattleEndDelegateEvent += petInitializerComponent.VanishPet;

        gameEvents.BattleLobbyEnteredDelegateEvent += SetPetInitializerProperties;

        petInitializerComponent.AddPetManager(petManager);
    }

    public override void Load(){}

    private void PetSummonEnabledCheck()
    {
        if (gameEvents.petEnable)
        {
            petInitializerComponent.SummonPet();
        }
    }

    private void SetPetInitializerProperties()
    {
        petInitializerComponent.position = gameEvents.petPosition;
        petInitializerComponent.rotation = gameEvents.petRotation;
    }
}
