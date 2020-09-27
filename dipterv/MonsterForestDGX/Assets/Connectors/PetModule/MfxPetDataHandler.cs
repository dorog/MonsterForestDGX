using UnityEngine;

//TODO: Remove it and impl it in DataManager?
public class MfxPetDataHandler : MonoBehaviour, IPetDataHandler
{
    public PetData[] LoadPetDatas()
    {
        DataManager dataManager = DataManager.GetInstance();
        return dataManager.GetPetDatas();
    }

    public void SavePetDatas(PetData[] petDatas)
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.SavePetDatas(petDatas);
    }
}
