using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public int id;
    public BattleManager battleManager;

    public GameObject root;
    public GameObject monster;

    private bool isMonster = true;

    private IEnemy enemy;

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

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
            battleManager.Battle(id, isMonster, gameObject);

            gameObject.SetActive(false);
        }
    }

    public void SetAlive(bool alive)
    {
        if (!alive)
        {
            enemy = monster.GetComponent<IEnemy>();
            enemy.Disable();
        }
    }
}
