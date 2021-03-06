﻿using UnityEngine;

public class BattlePlace : MonoBehaviour
{
    public int id;

    private EnemyType isMonster;

    public AiFighter enemy;
    public Health enemyHealth;

    //Merge them?
    public HealableConnector healable;
    public AttackableConnector attackable;

    public GameObject go;

    public GameEvents gameEvents;

    public GameObject petPosition = null;

    public bool petEnable = true;
    public bool resistantEnable = true;

    public BattleManager battleManager;

    public void Start()
    {
        if (enemy == null)
        {
            Debug.LogWarning("There is no IEnemy! Object: " + gameObject.name);
        }
        else
        {
            isMonster = enemy.IsMonster();
        }
    }

    public void Triggered()
    {
        gameEvents.id = id;
        gameEvents.enemyType = isMonster;
        gameEvents.battlePlace = this;
        gameEvents.enemy = enemy;
        gameEvents.healable = healable;
        gameEvents.attackable = attackable;
        gameEvents.petEnable = petEnable;
        gameEvents.resistantEnable = resistantEnable;
        gameEvents.enemyResistant = enemyHealth.resistant;

        if(petPosition != null)
        {
            gameEvents.petPosition = petPosition.transform.position;
            gameEvents.petRotation = petPosition.transform.rotation;
        }

        battleManager.Withdraw += ResetBattlePlaceState;
        battleManager.RedFighterWon += ResetBattlePlaceState;

        gameEvents.EnteredLobby();
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
        Invoke(nameof(EnableGo), 3f);
    }

    private void EnableGo()
    {
        go.SetActive(true);
    }
}
