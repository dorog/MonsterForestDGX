using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public int id;
    public BattleManager battleManager;

    public GameObject root;
    public GameObject monster;

    private bool isMonster = true;

    private IEnemy enemy;

    public GameObject go;

    private void Start()
    {
        enemy = monster.GetComponent<IEnemy>();
        if(enemy == null)
        {
            Debug.LogError("There is no IEnemy! Object: " + monster.name);
        }
        else
        {
            isMonster = enemy.IsMonster();
        }
    }

    public void Triggered()
    {
        battleManager.Battle(id, isMonster, this);
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
