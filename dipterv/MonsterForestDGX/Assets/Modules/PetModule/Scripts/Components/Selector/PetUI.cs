using UnityEngine;
using UnityEngine.UI;

public class PetUI : MonoBehaviour
{
    private int id;
    public Text petNameText;
    public GameObject selectedGo;

    public PetSelectorComponent petSelectorComponent;

    public void SetUI(int id, PetData petData, PetSelectorComponent _petSelectorComponent)
    {
        this.id = id;
        petNameText.text = petData.pet.petName;
        if (!petData.available)
        {
            gameObject.SetActive(false);
        }

        petSelectorComponent = _petSelectorComponent;
    }

    public void ChangePet()
    {
        petSelectorComponent.Select(selectedGo, id);
    }
}
