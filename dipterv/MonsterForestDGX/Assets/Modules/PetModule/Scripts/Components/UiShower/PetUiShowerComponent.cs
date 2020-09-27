using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetUiShowerComponent : PetComponent
{
    public Transform Content;
    public GameObject root;
    public Text petNameText;

    public PetAbilityDesciptionElementUI elementUI;

    public event Action uiActivated;
    public event Action uiDeactivated;

    public override void Init(IPetManager _petManager)
    {
        base.Init(_petManager);
        _petManager.SubscibeToPetDataChangedEvent(Refresh);
    }

    private void Refresh(List<PetDataDifference> petDataDifferences)
    {
        if (petDataDifferences != null && petDataDifferences.Count > 0)
        {
            if (petDataDifferences.Count > 1)
            {
                Debug.LogWarning(nameof(PetUiShowerComponent) + ": There is more than one diff, it will show only the last pet with diff!");
            }
            if (petDataDifferences[petDataDifferences.Count - 1].NewAvailability)
            {
                ShowUI(petDatas[petDataDifferences[petDataDifferences.Count - 1].Id].pet);
            }
        }
    }

    public void ShowUI(Pet pet)
    {
        ClearPrevious();

        var petName = pet.petName;
        var petAbilityDescriptions = pet.GetAbilityDesciptions();

        petNameText.text = petName;

        foreach (var petAbilityDescription in petAbilityDescriptions)
        {
            PetAbilityDesciptionElementUI instance = Instantiate(elementUI, Content);
            instance.Init(petAbilityDescription);
        }

        root.SetActive(true);

        Debug.Log("Add player movement to this");
        uiActivated?.Invoke();
    }

    private void ClearPrevious()
    {
        foreach(Transform child in Content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void HideUI()
    {
        root.SetActive(false);

        uiDeactivated?.Invoke();
    }
}
