using UnityEngine;

public class PetParameterConnection : MonoBehaviour
{
    [Header("Pet Parameters")]
    public AttackableTestImpl attackable;
    public HealableTestImpl healable;
    public ResetableTestImpl resetable;

    [Header("Dependencies")]
    public PetManager petManager;
    public PetDataHandlerTestImpl petDataLoader;
    public PetInitializerComponent petInitializerComponent;
    public PetSelectorComponent petSelectorComponent;
    public PetSelectorHandlerTestImpl petSelectorHandler;

    private void Start()
    {
        petManager.petDataHandler = petDataLoader;
        //petSelectorComponent.petSelectorHandler = petSelectorHandler;
        petInitializerComponent.petSelectorComponent = petSelectorComponent;
        petInitializerComponent.petParameter = GetPetParameters();
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
