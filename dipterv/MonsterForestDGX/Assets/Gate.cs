
using UnityEngine;

public class Gate : Fighter, IEnemy
{
    public BattleManager battleManager;
    public int id;

    public Controller controller;

    public ElementMovement doorMovement;
    public GateHealth gateHealth;
    public GameObject battlePlace;

    public ElementMovement[] crystals;

    public override void Die()
    {
        doorMovement.OpenContinously();
        battleManager.MonsterDied();
    }

    public void Fight()
    {
        controller.Step();
    }

    public Health GetHealth()
    {
        return health;
    }

    public bool IsMonster()
    {
        return false;
    }

    public void ResetMonster()
    {
        health.ResetHealth();
    }

    public void Appear()
    {
        foreach (var crystal in crystals)
        {
            crystal.OpenContinously();
        }
    }

    public void Disappear(){}

    public void Disable()
    {
        doorMovement.OpenInstantly();
        gateHealth.HideCrystals();
        battlePlace.SetActive(false);
    }
}
