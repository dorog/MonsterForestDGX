using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject monsterGO;
    public IEnemy monster;
    public Health monsterHealth;
    public Player player;

    public int id;
    private bool isMonster;

    private BattlePlace battlePlace;

    public bool petEnable = true;
    public bool resistantEnable = true;

    public GameObject petPosition;

    private BattleEvents battle;

    //Events

    public event Action PlayerTurnStartDelegateEvent;
    public event Action PlayerTurnEndDelegateEvent;
    public event Action MonsterTurnStartDelegateEvent;
    public event Action MonsterTurnEndDelegateEvent;

    public void Start()
    {
        battle = BattleEvents.GetInstance();
    }

    public void BattleLobby(int _id, bool _isMonster, BattlePlace battlePlace)
    {
        this.battlePlace = battlePlace;

        id = _id;
        isMonster = _isMonster;

        player.battleManager = this;

        monster = monsterGO.GetComponent<IEnemy>();
        monster.Appear();
        player.Battle(this, monsterHealth.resistant, petEnable, resistantEnable);
    }

    public void BattleStart()
    {
        battle.Fight(this);

        player.BattleStarted();
        monster.Fight();
    }

    public void PlayerTurn()
    {
        PlayerTurnStartDelegateEvent?.Invoke();
        MonsterTurnEndDelegateEvent?.Invoke();
    }

    public void MonsterTurn()
    {
        PlayerTurnEndDelegateEvent?.Invoke();
        MonsterTurnStartDelegateEvent?.Invoke();
    }

    public void MonsterDied()
    {
        battle.Explore(this);

        player.BattleEnd(id, isMonster);
    }

    public void PlayerDied()
    {
        battle.Explore(this);

        player.Died();

        monster.ResetMonster();
        battlePlace.ResetBattlePlace();
    }

    public void Run()
    {
        monster.Disappear();
        player.Run();
        battlePlace.ResetBattlePlace();
    }

    public void FinishedTraining()
    {
        battle.Explore(this);
        battlePlace.ResetBattlePlace();
    }

    public Vector3 GetPetPosition()
    {
        //Gate and Traning camp dont need it
        //TODO: Add option for use pets (cd or heal or ...)
        return petPosition.transform.position;
    }
}
