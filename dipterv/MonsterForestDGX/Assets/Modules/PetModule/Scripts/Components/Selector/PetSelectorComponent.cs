using System.Collections.Generic;
using UnityEngine;

public class PetSelectorComponent : PetComponent, IPetSelector
{
    private static readonly int notSelectedPetValue = -1;
    private int selectedPet = notSelectedPetValue;

    private IPetSelectorHandler petSelectorHandler = null;

    public int GetPet()
    {
        if (selectedPet == notSelectedPetValue)
        {
            return -1;
        }
        
        return selectedPet;
    }

    public override void AddPetManager(IPetManager _petManager)
    {
        base.AddPetManager(_petManager);
        _petManager.SubscibeToPetDataChangedEvent(Refresh);
        _petManager.SubscibeToPetDataLoadedEvent(SetupPets);
    }

    public void AddSelectorHandler(IPetSelectorHandler _petSelectorHandler)
    {
        petSelectorHandler = _petSelectorHandler;
    }

    private void Refresh(List<PetDataDifference> petDataDifferences)
    {
        if(petDataDifferences[0].Id == selectedPet && !petDataDifferences[0].NewAvailability)
        {
            selectedPet = notSelectedPetValue;
        }
    }

    private void SetupPets(PetData[] petDatas)
    {
        if (petDatas == null)
        {
            Debug.LogError("PetData: Null");
        }
        else
        {
            if(petSelectorHandler != null)
            {
                selectedPet = petSelectorHandler.GetLastSelectedPet();
            }

            if (selectedPet >= petDatas.Length || selectedPet < 0 || !petDatas[selectedPet].available)
            {
                selectedPet = notSelectedPetValue;
            }
        }
    }

    public void Select(int number)
    {
        if(selectedPet == number)
        {
            selectedPet = -1;
        }
        else if(selectedPet < petDatas.Length && selectedPet >= 0 && petDatas[number].available)
        {
            selectedPet = number;

            if (petSelectorHandler != null)
            {
                petSelectorHandler.Select(selectedPet);
            }
        }
    }
}
