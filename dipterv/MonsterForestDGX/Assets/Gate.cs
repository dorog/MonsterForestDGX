
using UnityEngine;

public class Gate : Fighter, IEnemy
{
    public BattleManager battleManager;
    public int id;

    public Controller controller;

    public GameObject obstacleGO;
    public GateHealth gateHealth;
    public GameObject battlePlace;

    public ElementMovement[] crystals;

    private IPuzzleMovement obstacle;

    public void Start()
    {
        obstacle = obstacleGO.GetComponent<IPuzzleMovement>();
    }

    public override void Die()
    {
        obstacle.DisappearContinously();
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
            crystal.DisappearContinously();
        }
    }

    public void Disappear(){}

    public void Disable()
    {
        if(obstacle == null)
        {
            obstacle = obstacleGO.GetComponent<IPuzzleMovement>();
        }
        obstacle.DisappearInstantly();
        gateHealth.HideCrystals();
        battlePlace.SetActive(false);
    }
}
