using System;
using UnityEngine;

public class BattleManager : SingletonClass<BattleManager>
{
    public IEnemy monster;
    public Health monsterHealth;
    public Player player;

    public int id;
    private bool isMonster;

    private BattlePlace battlePlace;

    public GameObject petPosition;

    private GameEvents gameEvents;

    //Events

    public event Action PlayerTurnStartDelegateEvent;
    public event Action PlayerTurnEndDelegateEvent;
    public event Action MonsterTurnStartDelegateEvent;
    public event Action MonsterTurnEndDelegateEvent;

    private void Awake()
    {
        Init(this);
    }

    public void Start()
    {
        gameEvents = GameEvents.GetInstance();

        gameEvents.BattleLobbyEnteredDelegateEvent += BattleLobby;
    }

    public void BattleLobby()
    {
        battlePlace = gameEvents.battlePlace;
        monster = gameEvents.enemy;
        monsterHealth = gameEvents.enemyHealth;
        player = gameEvents.player;
        petPosition = gameEvents.petPosition;

        id = gameEvents.id;
        isMonster = gameEvents.isMonster;

        monster.Appear();

        player.Battle(this, monsterHealth.resistant);
    }

    public void BattleStart()
    {
        gameEvents.Fight();

        monster.SubscribeToDie(MonsterDied);
        player.SubscribeToDie(PlayerDied);

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
        UnsubscribeDieFromDieEvents();

        gameEvents.Explore();

        player.BattleEnd(id, isMonster);
    }

    public void PlayerDied()
    {
        UnsubscribeDieFromDieEvents();

        gameEvents.Explore();

        player.PlayerDied();

        monster.ResetMonster();
        battlePlace.ResetBattlePlace();
    }

    private void UnsubscribeDieFromDieEvents()
    {
        monster.UnsubscribeToDie(MonsterDied);
        player.UnsubscribeToDie(PlayerDied);
    }

    public void Run()
    {
        monster.Disappear();
        player.Run();
        battlePlace.ResetBattlePlace();
    }

    public void FinishedTraining()
    {
        gameEvents.Explore();
        battlePlace.ResetBattlePlace();
    }

    public Vector3 GetPetPosition()
    {
        //Gate and Traning camp dont need it
        //TODO: Add option for use pets (cd or heal or ...)
        return petPosition.transform.position;
    }
}
