using UnityEngine;

public class PetParameterConnection : MonoBehaviour
{
    [Header("Pet Parameters")]
    public AttackableTestImpl attackable;
    public HealableTestImpl healable;
    public ResetableTestImpl resetable;

    [Header("Dependencies")]
    public PetSystem petSystem;
    public PetManager petManager;
    public PetDataLoaderTestImpl petDataLoader;
    public PetInitializerComponent petInitializerComponent;
    public PetSelectorComponent petSelectorComponent;
    public PetSelectorHandlerTestImpl petSelectorHandler;

    private void Start()
    {
        petManager.petDataHandler = petDataLoader;
        petSelectorComponent.petSelectorHandler = petSelectorHandler;
        petInitializerComponent.petSelectorComponent = petSelectorComponent;
        petInitializerComponent.petParameter = GetPetParameters();

        petSystem.InitializeComponents(petManager);
    }

    public PetParameter GetPetParameters()
    {
        return new PetParameter()
        {
            Attackable = attackable,
            Healable = healable,
            Resetable = resetable
        };
    }

    public IPetManager GetPetManagerImpl()
    {
        return petManager;
    }
}
