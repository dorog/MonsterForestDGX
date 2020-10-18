
using UnityEngine;

public class Gate : AiFighter
{
    public Controller controller;

    public GameObject obstacleGO;
    public GateHealth gateHealth;

    public ElementMovement[] crystals;

    public Health health;

    private IPuzzleMovement obstacle;

    public void Start()
    {
        obstacle = obstacleGO.GetComponent<IPuzzleMovement>();
    }

    public override void Die()
    {
        obstacle.DisappearContinously();
    }

    public override void Fight()
    {
        controller.StartController();
    }

    protected override void ResetMonster()
    {
        health.ResetHealth();
    }

    protected override void Appear()
    {
        foreach (var crystal in crystals)
        {
            crystal.DisappearContinously();
        }
    }

    protected override void Disappear(){}

    public override void Disable()
    {
        if(obstacle == null)
        {
            obstacle = obstacleGO.GetComponent<IPuzzleMovement>();
        }
        obstacle.DisappearInstantly();
        gateHealth.HideCrystals();
    }

    protected override void React(){}
}
