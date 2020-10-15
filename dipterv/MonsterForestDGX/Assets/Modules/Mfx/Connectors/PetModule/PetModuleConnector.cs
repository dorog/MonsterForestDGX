using UnityEngine;

public class PetModuleConnector : MonoBehaviour
{
    private PetParameter petParameter = new PetParameter();

    [Header ("Managers")]
    public PetManager petManager;
    public GameEvents gameEvents;

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
