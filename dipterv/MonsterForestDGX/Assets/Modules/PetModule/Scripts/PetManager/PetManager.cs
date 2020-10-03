using System;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour, IPetManager
{
    private event Action<PetData[]> LoadedPetData;
    private event Action<List<PetDataDifference>> ChangedPetData;

    private PetData[] petDatas;
    public IPetDataHandler petDataHandler;

    public void LoadData()
    {
        petDatas = petDataHandler.LoadPetDatas();
        LoadedPetData?.Invoke(petDatas);
    }

    //TODO: Add LockPet function
    public void UnlockPet(int id)
    {
        Debug.Log("Add LockPet function");
        bool originalValue = petDatas[id].available;

        petDatas[id].available = true;
        petDataHandler.SavePetDatas(petDatas);

        if (!originalValue)
        {

            ChangedPetData?.Invoke(new List<PetDataDifference>()
            {
                new PetDataDifference()
                {
                    Id = id,
                    OldAvailability = false,
                    NewAvailability = true
                }
            });
        }
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
