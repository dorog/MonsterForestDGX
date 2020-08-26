using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject monsterGO;
    public IEnemy monster;
    public Health monsterHealth;
    public Player player;
    public SceneLoader sceneLoader;

    public int id;
    private bool isMonster;

    private GameObject battlePlace;

    public delegate void MonsterTurnEndDelegate();
    public MonsterTurnEndDelegate monsterTurnStartDelegateEvent;

    public bool petEnable = true;
    public bool resistantEnable = true;

    public Controller controller;

    public GameObject petPosition;

    public void Battle(int _id, bool _isMonster, GameObject battlePlace)
    {
        this.battlePlace = battlePlace;

        id = _id;
        isMonster = _isMonster;
        //turnGO.SetActive(true);
        //turn.text = "Battle!";

        player.battleManager = this;

        monster = monsterGO.GetComponent<IEnemy>();
        monster.Appear();
        player.Battle(this, monsterHealth.resistant, petEnable, resistantEnable);
    }

    public void BattleStart()
    {
        player.BattleStarted();
        monster.Fight();
    }

    public void PlayerTurn()
    {
        player.AttackTurn();
        //turn.text = "Player Turn";
    }

    public void MonsterTurn()
    {
        player.DefTurn();

        monsterTurnStartDelegateEvent?.Invoke();
    }

    public void MonsterDied()
    {
        //turnGO.SetActive(false);
        player.BattleEnd(id, isMonster);
    }

    public void PlayerDied()
    {
        //turnGO.SetActive(false);

        player.Died();

        monster.ResetMonster();
        battlePlace.SetActive(true);

        //sceneLoader.LoadMainMenu();
    }

    public void Run()
    {
        //turnGO.SetActive(false);

        monster.Disappear();
        player.Run();
        //battlePlace.SetActive(true);
    }

    public void FinishedTraining()
    {
        //turnGO.SetActive(false);
        battlePlace.SetActive(true);
    }

    public Vector3 GetPetPosition()
    {
        //Gate and Traning camp dont need it
        return petPosition.transform.position;
    }
}
