using System;
using UnityEngine;

public class GameEvents : SingletonClass<GameEvents>
{
    public BattleManager battleManager;

    public Player player;

    public event Action BattleLobbyEnteredDelegateEvent;
    public event Action BattleStartDelegateEvent;
    public event Action BattleEndDelegateEvent;

    [Header("Battle Settings")]
    public bool isMonster = false;
    public int id;
    public BattlePlace battlePlace;
    public IEnemy enemy;
    public Health enemyHealth;
    public bool petEnable = false;
    public bool resistantEnable = false;
    public Resistant enemyResistant;
    public Vector3 petPosition;
    public Quaternion petRotation;

    public void Awake()
    {
        Init(this);
    }

    public void Fight()
    {
        BattleStartDelegateEvent?.Invoke();
    }

    public void Explore()
    {
        BattleEndDelegateEvent?.Invoke();
    }

    public void EnteredLobby()
    {
        BattleLobbyEnteredDelegateEvent?.Invoke();
    }
}
