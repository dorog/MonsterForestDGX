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
    public BattleManager battleManager;

    public GameObject[] extraObjects;
    public ParticleSystem[] extraParticles;

    private bool died = false;

    public TurnFill turnFill;

    public AutoController autoController;

    public override void StartTurn()
    {
        //turnFill.MoveForward();
    }

    public void React()
    {
        float random = Random.Range(0, 101);

        if (random <= blockChance)
        {
            health.SetDamageBlock();
        }
    }

    public override void Die()
    {
        died = true;
        battleManager.MonsterDied();
        animator.SetTrigger(dieAnimation);

        autoController.StopController();
    }

    public void Appear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(true);
        }

        animator.SetTrigger(appearAnimation);
        health.SetUpHealth();
        //nameText.text = MonsterName;
    }

    public void Disappear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(false);
        }
        foreach (var particle in extraParticles)
        {
            particle.Stop();
        }

        animator.SetTrigger(disappearAnimation);
    }

    public void ResetMonster()
    {
        autoController.StopController();

        Disappear();

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
        autoController.StartController();
    }
}
