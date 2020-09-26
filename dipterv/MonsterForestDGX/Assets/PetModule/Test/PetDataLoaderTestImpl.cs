using UnityEngine;

public class PetDataLoaderTestImpl : MonoBehaviour, IPetDataHandler
{
    public PetData[] petDatas;

    public PetData[] LoadPetDatas()
    {
        return petDatas;
    }

    public void SavePetDatas(PetData[] petDatas)
    {
        //Do nothing
    }
}
