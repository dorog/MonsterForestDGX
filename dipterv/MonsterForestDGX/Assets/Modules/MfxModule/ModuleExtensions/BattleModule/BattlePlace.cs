using System.Collections;
using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public int id;

    public AiFighter enemy;
    public Health enemyHealth;

    public GameObject battleTrigger;

    public GameEvents gameEvents;

    public BattleManager battleManager;

    [Header("Resistant Settings")]
    public bool resistantEnable = true;
    public Resistant resistant;

    [Header ("Pet Settings")]
    public bool petEnable = true;
    public AttackableHandler attackable;
    public GameObject petPosition = null;

    public void Triggered()
    {
        gameEvents.id = id;
        gameEvents.enemy = enemy;

        gameEvents.enemyResistant = resistant;
        gameEvents.resistantEnable = resistantEnable;

        battleManager.Withdraw += ResetBattlePlaceState;
        battleManager.RedFighterWon += ResetBattlePlaceState;

        SetPetSettings();

        gameEvents.EnteredLobby();
    }

    private void SetPetSettings()
    {
        attackable.health = enemyHealth;
        attackable.fighter = enemy;
        gameEvents.petEnable = petEnable;
        if (petPosition != null)
        {
            gameEvents.petPosition = petPosition.transform.position;
            gameEvents.petRotation = petPosition.transform.rotation;
        }
    }

    private void ResetBattlePlaceState()
    {
        battleManager.Withdraw -= ResetBattlePlaceState;
        battleManager.RedFighterWon -= ResetBattlePlaceState;
        ResetBattlePlace();
    }

    public void SetAlive(bool alive)
    {
        if (!alive)
        {
            enemy.Disable();
            gameObject.SetActive(false);
        }
    }

    public void ResetBattlePlace()
    {
        StartCoroutine(EnableGo());
    }

    private IEnumerator EnableGo()
    {
        yield return new WaitForSeconds(3);

        battleTrigger.SetActive(true);
    }
}
