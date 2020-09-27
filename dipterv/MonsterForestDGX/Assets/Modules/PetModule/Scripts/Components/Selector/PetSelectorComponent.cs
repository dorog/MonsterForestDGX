using System.Collections.Generic;
using UnityEngine;

public class PetSelectorComponent : PetComponent, IPetSelector
{
    public PetTab petTab;
    private GameObject selectedPetGO = null;
    private static readonly int notSelectedPetValue = -1;
    private int selectedPet = notSelectedPetValue;

    public IPetSelectorHandler petSelectorHandler;

    public int GetPet()
    {
        if (selectedPet == notSelectedPetValue)
        {
            return -1;
        }
        return selectedPet;
    }

    public override void Init(IPetManager _petManager)
    {
        base.Init(_petManager);
        _petManager.SubscibeToPetDataChangedEvent(Refresh);
        _petManager.SubscibeToPetDataLoadedEvent(SetupPets);
    }

    private void Refresh(List<PetDataDifference> petDataDifferences)
    {
        petTab.Refresh(petDataDifferences);
    }

    public void SetupPets(PetData[] petDatas)
    {
        if (petDatas == null)
        {
            Debug.LogError("PetData: Null");
        }
        else
        {
            selectedPet = petSelectorHandler.GetLastSelectedPet();
            if (selectedPet >= petDatas.Length)
            {
                selectedPet = notSelectedPetValue;
                petTab.SetUpUI(petDatas, this);
            }
            else
            {
                petTab.SetUpUI(petDatas, this, selectedPet);
            }
        }
    }

    public void Select(GameObject select, int number)
    {
        if (selectedPetGO != null)
        {
            selectedPetGO.SetActive(false);
        }
        selectedPetGO = select;
        selectedPetGO.SetActive(true);
        selectedPet = number;

        petSelectorHandler.Select(selectedPet);
    }
}
