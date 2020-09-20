﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Fighter, IEnemy
{
    public string MonsterName = "";
    public Text nameText;
    public Animator animator;
    public string appearAnimation;
    public float appearAnimationTime = 2;
    public string disappearAnimation;
    public float disappearAnimationTime = 2;
    public string dieAnimation;

    [Range(0, 100)]
    public float blockChance = 10f;

    public GameObject[] extraObjects;
    public ParticleSystem[] extraParticles;

    public TurnFill turnFill;

    public AutoController autoController;
    public AutoController playerDiedAutoController;

    public MagicCircleHandler magicCircleHandler;

    public GameObject root;

    public void React()
    {
        float random = UnityEngine.Random.Range(0, 101);

        if (random <= blockChance)
        {
            health.SetDamageBlock();
        }
    }

    public override void Die()
    {
        magicCircleHandler.successCastSpellDelegateEvent -= React;

        animator.SetTrigger(dieAnimation);

        autoController.StopController();

        base.Die();
    }

    public void Appear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(true);
        }

        animator.SetTrigger(appearAnimation);
        health.SetUpHealth();
    }

    public void Disappear()
    {
        DisableExtras();

        animator.SetTrigger(disappearAnimation);
    }

    private void FightReset()
    {
        DisableExtras();

        playerDiedAutoController.StartController();
    }

    private void DisableExtras()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(false);
        }
        foreach (var particle in extraParticles)
        {
            particle.Stop();
        }
    }

    public void ResetMonster()
    {
        magicCircleHandler.successCastSpellDelegateEvent -= React;

        autoController.StopController();

        FightReset();

        health.ResetHealth();
    }

    public Health GetHealth()
    {
        return GetComponent<MonsterHealth>();
    }

    public bool IsMonster()
    {
        return true;
    }

    public void Fight()
    {
        magicCircleHandler.successCastSpellDelegateEvent += React;
        autoController.StartController();
    }

    public void Disable()
    {
        root.SetActive(false);
    }
}
