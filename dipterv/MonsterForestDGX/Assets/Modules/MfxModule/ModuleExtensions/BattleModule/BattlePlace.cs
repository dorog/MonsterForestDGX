using System.Collections;
using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public int id;
    public string group;

    public AiFighter enemy;
    public Player player;
    public Health enemyHealth;

    public GameObject battleTrigger;

    public GameEvents gameEvents;

    public BattleManager battleManager;
    public Controller controller;

    [Header("Resistant Settings")]
    public bool resistantEnable = true;
    public Resistant resistant;

    [Header ("Pet Settings")]
    public bool petEnable = true;
    public AttackableHandler attackable;
    public GameObject petPosition = null;
    public ParticleSystem petAttackEffect;

    public void Triggered()
    {
        gameEvents.id = id;
        gameEvents.group = group;
        gameEvents.enemy = enemy;

        gameEvents.enemyResistant = resistant;
        gameEvents.resistantEnable = resistantEnable;

        battleManager.Withdraw += ResetBattlePlaceState;
        battleManager.RedFighterWon += ResetBattlePlaceState;
        battleManager.controller = controller;

        SetPetSettings();

        gameEvents.EnteredLobby();
    }

    private void SetPetSettings()
    {
        attackable.health = enemyHealth;
        attackable.petAttackEffect = petAttackEffect;

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
