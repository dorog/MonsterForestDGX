using UnityEngine;

public class PetInitializerConnector : AbstractConnector
{
    public PetInitializerComponent petInitializerComponent;

    public GameEvents gameEvents;
    
    public PetManager petManager;

    [Tooltip ("Must have an IPetSelector component!")]
    public GameObject petSelectorGO;

    [Header("Pet Parameter Settings")]
    public HealableHandler healable;
    public AttackableHandler attackable;
    public ResetableHandler resetable;

    private PetParameter petParameter;

    public override void Setup()
    {
        IPetSelector petSelector = petSelectorGO.GetComponent<IPetSelector>();

        petParameter = new PetParameter()
        {
            Attackable = attackable,
            Healable = healable,
            Resetable = resetable
        };

        petInitializerComponent.petSelectorComponent = petSelector;
        petInitializerComponent.petParameter = petParameter;

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
        petParameter.Attackable = attackable;
    }
}
