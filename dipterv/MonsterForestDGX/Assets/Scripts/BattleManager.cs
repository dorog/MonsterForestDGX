﻿using UnityEngine;
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

    private BattlePlace battlePlace;

    public delegate void MonsterTurnEndDelegate();
    public MonsterTurnEndDelegate monsterTurnStartDelegateEvent;

    public bool petEnable = true;
    public bool resistantEnable = true;

    public Controller controller;

    public GameObject petPosition;

    public void Battle(int _id, bool _isMonster, BattlePlace battlePlace)
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
        player.BattleStarted();
        monster.Fight();
    }

    public void PlayerTurn()
    {
        player.AttackTurn();
    }

    public void MonsterTurn()
    {
        player.DefTurn();

        monsterTurnStartDelegateEvent?.Invoke();
    }

    public void MonsterDied()
    {
        player.BattleEnd(id, isMonster);
    }

    public void PlayerDied()
    {
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
        battlePlace.ResetBattlePlace();
    }

    public Vector3 GetPetPosition()
    {
        //Gate and Traning camp dont need it
        //TODO: Add option for use pets (cd or heal or ...)
        return petPosition.transform.position;
    }
}
