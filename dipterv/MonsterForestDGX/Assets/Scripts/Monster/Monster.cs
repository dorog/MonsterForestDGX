using UnityEngine;

public class Monster : AiFighter
{
    public string MonsterName = "";
    public Animator animator;
    public string appearAnimation;
    public float appearAnimationTime = 2;
    public string disappearAnimation;
    public float disappearAnimationTime = 2;
    public string dieAnimation;

    public Health health;

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
        float random = Random.Range(0, 101);

        if (random <= blockChance)
        {
            health.SetDamageBlock();
        }
    }

    public override void Die()
    {
        magicCircleHandler.SuccessCastSpellDelegateEvent -= React;

        animator.SetTrigger(dieAnimation);

        autoController.StopController();

        base.Die();
    }

    protected override void Appear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(true);
        }

        animator.SetTrigger(appearAnimation);
        health.SetUpHealth();
    }

    protected override void Disappear()
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

    protected override void ResetMonster()
    {
        magicCircleHandler.SuccessCastSpellDelegateEvent -= React;

        autoController.StopController();

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

    public override void SetupForFight()
    {
        Appear();
    }

    public override void Fight()
    {
        magicCircleHandler.SuccessCastSpellDelegateEvent += React;
        autoController.StartController();
    }
}
