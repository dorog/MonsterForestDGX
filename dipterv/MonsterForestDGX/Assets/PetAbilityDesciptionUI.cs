using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetAbilityDesciptionUI : MonoBehaviour
{
    public Transform Content;
    public GameObject root;
    public Text petNameText;

    public PetAbilityDesciptionElementUI elementUI;

    public Player player;

    public void ShowUI(string petName, List<PetAbilityDesciption> petAbilityDesciptions)
    {
        petNameText.text = petName;

        foreach (var petAbilityDescription in petAbilityDesciptions)
        {
            PetAbilityDesciptionElementUI instance = Instantiate(elementUI, Content);
            instance.Init(petAbilityDescription);
        }

        player.MenuState(true);

        root.SetActive(true);
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
        player.MenuState(false);

        ClearPrevious();
        root.SetActive(false);
    }
}
