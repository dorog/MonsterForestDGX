using UnityEngine;
using UnityEngine.UI;

public class PetUI : MonoBehaviour
{
    private int id;
    public Text petNameText;
    public GameObject selectedGo;

    public void SetUI(int id, Pet pet)
    {
        this.id = id;
        petNameText.text = pet.petName;
    }

    public void ChangePet()
    {
        PetManager petManager = PetManager.GetInstance();
        petManager.Select(selectedGo, id);
    }
}
