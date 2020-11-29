using System;
using System.Collections.Generic;
using UnityEngine;

//TODO: Separate Functions based on the components
public class MfxPetManager : MonoBehaviour, IPetManager
{
    private event Action<PetData[]> LoadedPetData;
    private event Action<List<PetDataDifference>> ChangedPetData;

    private PetData[] petDatas;
    private IPetDataHandler petDataHandler;

    public void AddPetDataHandler(IPetDataHandler _petDataHandler)
    {
        petDataHandler = _petDataHandler;
    }

    public void LoadData()
    {
        petDatas = petDataHandler.LoadPetDatas();
        LoadedPetData?.Invoke(petDatas);
    }

    public void ChangePetFunction(int id)
    {
        petDatas[id].available = !petDatas[id].available;
        petDataHandler.SavePetDatas(petDatas);

        ChangedPetData?.Invoke(new List<PetDataDifference>()
        {
            new PetDataDifference()
            {
                Id = id,
                OldAvailability = !petDatas[id].available,
                NewAvailability = petDatas[id].available
            }
        });
    }

    public void SubscibeToPetDataLoadedEvent(Action<PetData[]> method)
    {
        LoadedPetData += method;
    }

    public void UnsubscibeToPetDataLoadedEvent(Action<PetData[]> method)
    {
        LoadedPetData -= method;
    }

    public void SubscibeToPetDataChangedEvent(Action<List<PetDataDifference>> method)
    {
        ChangedPetData += method;
    }

    public void UnsubscibeToPetDataChangedEvent(Action<List<PetDataDifference>> method)
    {
        ChangedPetData -= method;
    }
}
