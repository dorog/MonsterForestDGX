using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public int id;
    public BattleManager battleManager;

    public GameObject monster;

    private bool isMonster = true;

    private IEnemy enemy;
    private Health enemyHealth;

    public GameObject go;

    private GameEvents gameEvents;

    public GameObject petPosition;

    public bool petEnable = true;
    public bool resistantEnable = true;

    private void Start()
    {
        enemy = monster.GetComponent<IEnemy>();
        enemyHealth = monster.GetComponent<Health>();

        if (enemy == null)
        {
            Debug.LogError("There is no IEnemy! Object: " + monster.name);
        }
        else
        {
            isMonster = enemy.IsMonster();
        }

        gameEvents = GameEvents.GetInstance();
    }

    public void Triggered()
    {
        gameEvents.id = id;
        gameEvents.isMonster = isMonster;
        gameEvents.battlePlace = this;
        gameEvents.enemy = enemy;
        gameEvents.enemyHealth = enemyHealth;
        gameEvents.petEnable = petEnable;
        gameEvents.petPosition = petPosition;
        gameEvents.resistantEnable = resistantEnable;
        gameEvents.enemyResistant = enemyHealth.resistant;

        gameEvents.EnteredLobby();
    }

    public void SetAlive(bool alive)
    {
        if (!alive)
        {
            enemy = monster.GetComponent<IEnemy>();
            enemy.Disable();
        }
    }

    public void ResetBattlePlace()
    {
        Invoke(nameof(EnableGo), 3f);
    }

    private void EnableGo()
    {
        go.SetActive(true);
    }
}
