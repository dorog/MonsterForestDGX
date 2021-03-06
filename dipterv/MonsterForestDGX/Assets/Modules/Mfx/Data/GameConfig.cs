﻿using UnityEngine;

[CreateAssetMenu(fileName = "new GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player Settings")]
    public float exp;

    [Header("Monster Settings")]
    public int aliveMonsters;

    [Header("Gate Settings")]
    public int gatesState;

    [Header("Teleports Settings")]
    public int teleports;

    [Header("Attack Spells Settings")]
    public SpellConfig[] baseSpells;

    [Header("Pet Settings")]
    public Pet[] pets;
    public int lastSelectedPet = -1;
}
