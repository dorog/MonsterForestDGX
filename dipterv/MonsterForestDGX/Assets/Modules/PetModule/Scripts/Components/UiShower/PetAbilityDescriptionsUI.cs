using System.Collections.Generic;
using UnityEngine;

public class PetAbilityDescriptionsUI : MonoBehaviour
{
    public PetAbilityDesciptionElementUI elementUI;

    public void ShowAbilityDescriptions(List<PetAbilityDesciption> petAbilityDesciptions)
    {
        ClearPrevious();

        foreach (var petAbilityDescription in petAbilityDesciptions)
        {
            PetAbilityDesciptionElementUI instance = Instantiate(elementUI, transform);
            instance.Init(petAbilityDescription);
        }
    }

    private void ClearPrevious()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
