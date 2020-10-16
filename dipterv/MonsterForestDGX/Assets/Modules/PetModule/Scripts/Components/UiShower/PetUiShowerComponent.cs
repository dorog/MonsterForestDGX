using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetUiShowerComponent : PetComponent
{
    public GameObject root;
    public Text petNameText;

    public PetAbilityDescriptionsUI petElementDescriptionsUI;

    public override void AddPetManager(IPetManager _petManager)
    {
        base.AddPetManager(_petManager);
        _petManager.SubscibeToPetDataChangedEvent(Refresh);
    }

    private void Refresh(List<PetDataDifference> petDataDifferences)
    {
        if (petDataDifferences != null && petDataDifferences.Count > 0)
        {
            if (petDataDifferences[petDataDifferences.Count - 1].NewAvailability)
            {
                ShowUI(petDatas[petDataDifferences[petDataDifferences.Count - 1].Id].pet);
            }
        }
    }

    private void ShowUI(Pet pet)
    {
        var petName = pet.petName;
        var petAbilityDescriptions = pet.GetAbilityDesciptions();

        petNameText.text = petName;

        petElementDescriptionsUI.ShowAbilityDescriptions(petAbilityDescriptions);

        root.SetActive(true);
    }

    public void HideUI()
    {
        root.SetActive(false);
    }
}
