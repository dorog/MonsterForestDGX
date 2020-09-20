using UnityEngine;

public class PetHandler : MonoBehaviour
{
    private bool petEnable = true;
    private PetManager petManager;
    private GameObject petGO;

    public BattleManager battleManager;

    public Player player;
    public GameEvents gameEvents;

    void Start()
    {
        petManager = PetManager.GetInstance();
        gameEvents = GameEvents.GetInstance();

        gameEvents.BattleStartDelegateEvent += Fighting;
        gameEvents.BattleEndDelegateEvent += DestroyPet;
    }

    private void Fighting()
    {
        petEnable = gameEvents.petEnable;
        InstantiatePet();
    }

    public void InstantiatePet()
    {
        if (petGO != null)
        {
            Destroy(petGO);
        }

        if (petEnable)
        {
            GameObject playerPet = petManager.GetPet();
            if (playerPet != null)
            {
                petGO = Instantiate(playerPet, battleManager.GetPetPosition(), transform.rotation);

                Pet pet = petGO.GetComponent<Pet>();
                pet.AddPlayer(player);
            }
        }
    }

    public void DestroyPet()
    {
        if (petGO != null)
        {
            Destroy(petGO);
        }
    }
}
