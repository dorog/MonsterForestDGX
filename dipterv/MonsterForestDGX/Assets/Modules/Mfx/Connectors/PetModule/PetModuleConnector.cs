using UnityEngine;

public class PetModuleConnector : MonoBehaviour
{
    private PetParameter petParameter = new PetParameter();

    [Header ("Managers")]
    public PetSystem petSystem;
    public PetManager petManager;
    public GameEvents gameEvents;

    [Header ("Components / Dependencies")]

    [Header("Dependencies (Not component)")]
    public MfxPetDataHandler petDataHandler;
    public MfxPetSelectorHandler petSelectorHandler;
    public MagicCircleHandler resetable;
    public HealableConnector healable;

    [Header("Event Assignments")]
    public PetTab petTab;

    public void Start()
    {
        AddDependencies();
        AssignToInputs();

        petSystem.InitializeComponents(petManager);
    }

    private void AddDependencies()
    {
        petManager.petDataHandler = petDataHandler;



        petParameter.Resetable = resetable;
        petParameter.Healable = healable;
    }

    private void AssignToInputs()
    {

    }
}
