using UnityEngine;

public class Monster : AiFighter
{
    public Animator animator;
    public string dieAnimation;
    public MonsterHealth health;

    [Header ("Appear Settings")]
    public string appearAnimation;
    public float appearAnimationTime = 2;

    [Header("Disappear Settings")]
    public string disappearAnimation;
    public float disappearAnimationTime = 2;

    [Range(0, 100)]
    public float blockChance = 10f;

    public GameObject[] extraObjects;
    public ParticleSystem[] extraParticles;

    public AutoController EnemyDiedAutoController;

    public GameObject root;

    public MonsterAttack monsterAttack;

    protected override void React()
    {
        float random = Random.Range(1, 101);

        if (random <= blockChance)
        {
            health.SetDamageBlock();
        }
    }

    public override void Die()
    {
        animator.SetTrigger(dieAnimation);

        base.Die();
    }

    protected override void Appear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(true);
        }

        animator.SetTrigger(appearAnimation);
    }

    protected override void Disappear()
    {
        DisableExtras();

        animator.SetTrigger(disappearAnimation);
    }

    private void FightReset()
    {
        DisableExtras();

        EnemyDiedAutoController.StartController();
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

    protected override void ResetMonster()
    {
        FightReset();

        health.ResetHealth();
    }

    public override EnemyType IsMonster()
    {
        return EnemyType.Monster;
    }

    public override void Disable()
    {
        root.SetActive(false);
    }

    public override void SetupForFight(Fighter fighter)
    {
        base.SetupForFight(fighter);
        health.Init();
    }
}
