﻿using System;
using UnityEngine;

public class Player : Fighter
{
    public PlayerHealth playerHealth;

    public event Action Stopped;
    public event Action Go;

    [Header("UI")]
    public GameObject leftHandCanvas;
    public GameObject rightHandCanvas;

    private bool isStopped = false;

    public override void Die()
    {
        GoCall("Player Die");

        base.Die();
    }

    //Refactor
    public void MenuState()
    {
        if (!isStopped)
        {
            GoCall("MenuS");
            //Stopped?.Invoke();
        }
        else
        {
            GoCall("MenuG");
            //Go?.Invoke();
        }

        isStopped = !isStopped;
    }

    public void EnableControlling()
    {
        Go?.Invoke();
    }

    public void Run()
    {
        GoCall("Run");
        //Go?.Invoke();

        DisableUI();
    }

    public void FinishedTraining()
    {
        GoCall("FT");
        //Go?.Invoke();

        DisableUI();
    }

    public override void SetupForFight(Fighter fighter)
    {
        Debug.Log("Got the enemy: Maybe warning when it attacks?");
        StopCall("Battle");

        playerHealth.InitHealth();

        leftHandCanvas.SetActive(true);
        rightHandCanvas.SetActive(true);
    }

    public override void Win()
    {
        GoCall("BTE");
        //Go?.Invoke();

        DisableUI();
    }

    private void GoCall(string name)
    {
        Debug.Log(name);
        Go?.Invoke();
    }

    private void StopCall(string name)
    {
        Debug.Log(name);
        Stopped?.Invoke();
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
