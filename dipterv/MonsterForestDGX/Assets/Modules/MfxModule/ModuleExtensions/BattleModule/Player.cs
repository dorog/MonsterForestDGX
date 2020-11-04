using System;
using UnityEngine;

public class Player : Fighter
{
    public PlayerHealth playerHealth;

    public event Action Stop;
    public event Action Go;

    [Header("UI")]
    public GameObject leftHandCanvas;
    public GameObject rightHandCanvas;

    public override void Die()
    {
        Go?.Invoke();

        base.Die();
    }

    public void EnableControlling()
    {
        Go?.Invoke();
    }

    public void Run()
    {
        Go?.Invoke();

        DisableUI();
    }

    public void FinishedTraining()
    {
        Go?.Invoke();

        DisableUI();
    }

    public override void SetupForFight(Fighter fighter)
    {
        Debug.Log("Got the enemy: Maybe warning when it attacks?");
        Stop?.Invoke();

        playerHealth.InitHealth();

        leftHandCanvas.SetActive(true);
        rightHandCanvas.SetActive(true);
    }

    public override void FightStarted(){}

    public override void Win()
    {
        Go?.Invoke();

        DisableUI();
    }

    public override void Withdraw()
    {
        Run();
    }

    public override void Draw()
    {
        DisableUI();
        Run();
    }

    private void DisableUI()
    {
        leftHandCanvas.SetActive(false);
        rightHandCanvas.SetActive(false);
    }

    protected override void React(){}
}
