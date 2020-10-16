using UnityEngine;


//TODO: Remove this class
public class PetModuleConnector : MonoBehaviour
{
    private PetParameter petParameter = new PetParameter();

    public HealableHandler healable;

    public void Start()
    {
        AddDependencies();
    }

    private void AddDependencies()
    {
        petParameter.Healable = healable;
    }
}
