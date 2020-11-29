using System.Collections.Generic;
using UnityEngine;

public class PetAbilityDescriptionsUI : MonoBehaviour
{
    public PetAbilityDesciptionElementUI elementUI;
    public Transform content;

    public void ShowAbilityDescriptions(List<PetAbilityDesciption> petAbilityDesciptions)
    {
        ClearPrevious();

        foreach (var petAbilityDescription in petAbilityDesciptions)
        {
            PetAbilityDesciptionElementUI instance = Instantiate(elementUI, content);
            instance.Init(petAbilityDescription);
        }
    }

    private void ClearPrevious()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
