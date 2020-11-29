using System.Collections.Generic;
using UnityEngine;

public class MfxPetSelector : PetSelectorComponent
{
    public PetTab petTab;
    private GameObject selectedPetGO = null;

    public override void AddPetManager(IPetManager _petManager)
    {
        base.AddPetManager(_petManager);
        _petManager.SubscibeToPetDataChangedEvent(MfxRefresh);
        _petManager.SubscibeToPetDataLoadedEvent(MfxSetupPets);
    }

    private void MfxRefresh(List<PetDataDifference> petDataDifferences)
    {
        petTab.Refresh(petDataDifferences);
    }

    private void MfxSetupPets(PetData[] petDatas)
    {
        petTab.SetUpUI(petDatas, this, GetPet());
    }

    public void Select(GameObject select, int number)
    {
        if (selectedPetGO != null)
        {
            selectedPetGO.SetActive(false);
        }

        int selected = GetPet();

        if(number == selected)
        {
            selectedPetGO = null;
        }
        else
        {
            selectedPetGO = select;
            selectedPetGO.SetActive(true);
        }

        Select(number);
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
