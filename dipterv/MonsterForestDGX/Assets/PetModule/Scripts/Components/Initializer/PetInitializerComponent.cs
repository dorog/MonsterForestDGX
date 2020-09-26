using UnityEngine;

public class PetInitializerComponent : PetComponent
{
    private Pet petInstance;

    public Vector3 position;

    //public GameEvents gameEvents;

    public PetParameter petParameter;

    public IPetSelector petSelectorComponent;

    //Add To Connector class
    void Start()
    {
        /*petManager = PetManager.GetInstance();
        gameEvents = GameEvents.GetInstance();

        gameEvents.BattleStartDelegateEvent += Fighting;
        gameEvents.BattleEndDelegateEvent += DestroyPet;*/
        //position = ... (before instantiate)
        Debug.Log("Add To Connector class");
        Debug.Log("Not use PetSelectorComponent, add interface for it: abstract connection class");
    }

    public void SummonPet()
    {
        VanishPet();

        int petId = petSelectorComponent.GetPet();
        if (petId >= 0 && petDatas.Length > petId && petDatas[petId].available)
        {
            petInstance = Instantiate(petDatas[petId].pet, position, transform.rotation);
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
