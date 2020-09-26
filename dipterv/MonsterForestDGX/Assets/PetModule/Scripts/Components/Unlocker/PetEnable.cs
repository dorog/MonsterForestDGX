using UnityEngine;

public class PetEnable : MonoBehaviour
{
    private int id = -1;
    private PetUnlockerComponent petUnlockerComponent;

    private Pet pet;

    private bool collected = false;

    public GameObject availableSign;
    public Transform petPosition;

    public void Setup(PetData _petData, int _id, PetUnlockerComponent _petUnlockerComponent)
    {
        pet = Instantiate(_petData.pet, petPosition);
        pet.gameObject.SetActive(_petData.available);

        id = _id;
        petUnlockerComponent = _petUnlockerComponent;
    }

    public void Refresh(bool available)
    {
        pet.gameObject.SetActive(available);
        if(!collected && !available)
        {
            availableSign.SetActive(false);
        }

        collected = available;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Call only collect, dont add extra logic?");
        if (!collected && other.gameObject.tag == "Player")
        {
            petUnlockerComponent.SetAvailablePet(id);
            availableSign.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collected && other.gameObject.tag == "Player")
        {
            petUnlockerComponent.DisableAvailablePet();
            availableSign.SetActive(false);
        }
    }

    public void Collected()
    {
        pet.gameObject.SetActive(false);
        collected = true;
        availableSign.SetActive(false);
    }
}
