using UnityEngine;

public class Player : Fighter
{
    public PlayerHealth playerHealth;

    [Header("UI")]
    public GameObject leftHandCanvas;
    public GameObject rightHandCanvas;

    public void Run()
    {
        DisableUI();
    }

    public void FinishedTraining()
    {
        DisableUI();
    }

    public override void SetupForFight(Fighter fighter)
    {
        Debug.Log("Got the enemy: Maybe warning when it attacks?");
        playerHealth.InitHealth();

        leftHandCanvas.SetActive(true);
        rightHandCanvas.SetActive(true);
    }

    public override void FightStarted(){}

    public override void Win()
    {
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
