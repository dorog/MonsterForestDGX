using System.Collections.Generic;
using UnityEngine;

public class PetSelectorComponent : PetComponent, IPetSelector
{
    public PetTab petTab;
    private GameObject selectedPetGO = null;
    private static readonly int notSelectedPetValue = -1;
    private int selectedPet = notSelectedPetValue;

    private IPetSelectorHandler petSelectorHandler;

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
        petTab.Refresh(petDataDifferences);
    }

    private void SetupPets(PetData[] petDatas)
    {
        if (petDatas == null)
        {
            Debug.LogError("PetData: Null");
        }
        else
        {
            selectedPet = petSelectorHandler.GetLastSelectedPet();
            if (selectedPet >= petDatas.Length || selectedPet < 0)
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

        if(selectedPet == number)
        {
            selectedPetGO = null;
            selectedPet = -1;
        }
        else
        {
            selectedPetGO = select;
            selectedPetGO.SetActive(true);
            selectedPet = number;
        }


        petSelectorHandler.Select(selectedPet);
    }

    public void ShowSelectorTab()
    {
        petTab.ShowTab();
    }

    public void HideSelectorTab()
    {
        petTab.HideTab();
    }
}
