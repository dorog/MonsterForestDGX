using UnityEngine;

public class PetInitializerConnector : MonoBehaviour
{
    public PetInitializerComponent petInitializerComponent;
    public PetSelectorComponent petSelectorComponent;

    public GameEvents gameEvents;

    [Header("Pet Parameter Settings")]
    public HealableConnector healable;
    public AttackableConnector attackable;
    public MagicCircleHandler resetable;

    private PetParameter petParameter;

    private void Start()
    {
        petParameter = new PetParameter()
        {
            Attackable = attackable,
            Healable = healable,
            Resetable = resetable
        };

        petInitializerComponent.petSelectorComponent = petSelectorComponent;
        petInitializerComponent.petParameter = petParameter;

        gameEvents.BattleStartDelegateEvent += PetSummonEnabledCheck;
        gameEvents.BattleEndDelegateEvent += petInitializerComponent.VanishPet;

        gameEvents.BattleLobbyEnteredDelegateEvent += SetPetInitializerProperties;
    }

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
