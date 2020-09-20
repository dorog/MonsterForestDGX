using UnityEngine;

public class PetManager : SingletonClass<PetManager>
{
    private GameObject selectedPetGO = null;
    private readonly int notSelectedPetValue = -1;
    public int selectedPet;
    private Pet[] pets;

    public PetTab petTab;

    public Sprite selectedImageSprite;

    private static readonly string lastPetKey = "lastPetKey";

    private static readonly int defaultPetId = -1;
    private int availablePetId = defaultPetId;
    private PetEnable actualPetEnable = null;

    //TODO: Add to another class (like PetEnable)
    public IPressed collectInput;

    public void Awake()
    {
        Init(this);
    }

    public void Start()
    {
        SetupPets();
        collectInput = KeyBindingManager.GetInstance().petCollectButton;

        collectInput.SubscribeToPressed(Collect);
    }

    private void SetupPets()
    {
        pets = DataManager.GetInstance().GetAvailablePets();
        if (pets == null)
        {
            Debug.LogError("PetManager: Null");
        }
        else
        {
            selectedPet = PlayerPrefs.GetInt(lastPetKey, notSelectedPetValue);
            if (selectedPet >= pets.Length)
            {
                selectedPet = notSelectedPetValue;
                petTab.SetUpUI(pets);
            }
            else
            {
                petTab.SetUpUI(pets, selectedPet);
            }
        }
    }

    public GameObject GetPet()
    {
        if(selectedPet == notSelectedPetValue)
        {
            return null;
        }
        return pets[selectedPet].gameObject;
    }

    public void Select(GameObject select, int number)
    {
        if(selectedPetGO != null)
        {
            selectedPetGO.SetActive(false);
        }
        selectedPetGO = select;
        selectedPetGO.SetActive(true);
        selectedPet = number;

        //TODO: Change
        PlayerPrefs.SetInt(lastPetKey, selectedPet);
    }

    public Pet[] GetPets()
    {
        return pets;
    }

    private void Collect()
    {
        if (availablePetId != defaultPetId)
        {
            CollectPet();
            DisableAvailablePet();
        }
    }

    public void SetAvailablePet(PetEnable petEnable, int id)
    {
        availablePetId = id;
        actualPetEnable = petEnable;
    }

    public void DisableAvailablePet()
    {
        availablePetId = defaultPetId;
    }

    private void CollectPet()
    {
        DataManager.GetInstance().CollectPet(availablePetId);
        actualPetEnable.Collected();
        SetupPets();
    }
}
