using UnityEngine;

public class PetSystem : MonoBehaviour
{
    public PetComponent[] petComponents;

    public void InitializeComponents(IPetManager petManager)
    {
        foreach(var petComponent in petComponents)
        {
            petComponent.Init(petManager);
        }

        petManager.LoadData();
    }
}
