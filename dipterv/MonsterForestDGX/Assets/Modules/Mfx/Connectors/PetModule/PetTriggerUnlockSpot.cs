using UnityEngine;

public class PetTriggerUnlockSpot : PetUnlockSpot
{
    private int id = -1;
    public PetUnlockerComponent petUnlockerComponent;

    private Pet pet;

    private bool collected = false;
    private bool inArea = false;

    public GameObject availableSign;
    public Transform petPosition;
    //public LayerMask layerMask;

    public string colliderTag = "Player";

    public IPressed collectInput;

    //TODO: Remove later
    public KeyBindingManager keyBindingManager;

    public void Start()
    {
        collectInput = keyBindingManager.petCollectButton;
    }

    private void Collect()
    {
        if(inArea && !collected)
        {
            petUnlockerComponent.CollectPet(id);
            collectInput.UnsubscribeFromPressed(Collect);
        }
    }

    public override void Setup(PetData _petData, int _id, PetUnlockerComponent _petUnlockerComponent)
    {
        pet = Instantiate(_petData.pet, petPosition);
        pet.gameObject.SetActive(!_petData.available);
        collected = _petData.available;
        if (!collected)
        {
            collectInput = keyBindingManager.petCollectButton;
            collectInput.SubscribeToPressed(Collect);
        }

        id = _id;
        petUnlockerComponent = _petUnlockerComponent;
    }

    public override void Refresh(bool available)
    {
        pet.gameObject.SetActive(!available);
        if(!inArea || available)
        {
            availableSign.SetActive(false);
        }

        collected = available;
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (!collected && other.gameObject.CompareTag(colliderTag))
        {
            availableSign.SetActive(true);
            collectInput.Activate();
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collected && other.gameObject.CompareTag(colliderTag))
        {
            availableSign.SetActive(false);
            collectInput.Deactivate();
            inArea = false;
        }
    }
}
