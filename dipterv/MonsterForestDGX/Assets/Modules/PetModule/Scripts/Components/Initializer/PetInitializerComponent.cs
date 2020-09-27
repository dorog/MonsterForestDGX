using UnityEngine;

public class PetInitializerComponent : PetComponent
{
    private Pet petInstance;

    public IPetSelector petSelectorComponent;

    public Vector3 position;
    public Quaternion rotation;
    public PetParameter petParameter;

    public void SummonPet()
    {
        VanishPet();

        int petId = petSelectorComponent.GetPet();
        if (petId >= 0 && petDatas.Length > petId && petDatas[petId].available)
        {
            petInstance = Instantiate(petDatas[petId].pet, position, rotation);
            petInstance.Init(petParameter);
        }
    }

    public void VanishPet()
    {
        if (petInstance != null)
        {
            Destroy(petInstance.gameObject);
        }
    }
}
