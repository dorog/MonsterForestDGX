using UnityEngine;

public class MfxPetDataHandler : MonoBehaviour, IPetDataHandler
{
    public DataManager dataManager;

    public PetData[] LoadPetDatas()
    {
        return dataManager.GetPetDatas();
    }

    public void SavePetDatas(PetData[] petDatas)
    {
        dataManager.SavePetDatas(petDatas);
    }
}
