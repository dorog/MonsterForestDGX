﻿using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public Fighter player;

    public event Action BattleLobbyEnteredDelegateEvent;
    public event Action BattleStartDelegateEvent;
    public event Action BattleEndDelegateEvent;

    [Header("Battle Settings")]
    public int id;
    public BattlePlace battlePlace;
    public Fighter enemy;

    public bool resistantEnable = false;
    public Resistant enemyResistant;
    public Vector3 petPosition;
    public Quaternion petRotation;

    public bool petEnable = false;

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

    public Fighter GetRedFighter()
    {
        return enemy;
    }

    public Fighter GetBlueFighter()
    {
        return player;
    }
}
