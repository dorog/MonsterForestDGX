using UnityEngine;

public class AiFighter : Fighter
{
    public Health health;

    public GameObject[] extraObjects;
    public ParticleSystem[] extraParticles;

    public GameObject root;

    private void Start()
    {
        health.Die += Die;
    }

    protected override void React()
    {
        health.SetDamageBlock();
    }

    protected virtual void Appear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(true);
        }
    }

    protected virtual void Disappear()
    {
        health.DisapperHealth();
        DisableExtras();
    }

    protected virtual void ResetMonster()
    {
        DisableExtras();

        health.InitHealth();
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

    public virtual void Disable()
    {
        root.SetActive(false);
        health.DisableHealth();
    }

    public override void SetupForFight(Fighter fighter)
    {
        base.SetupForFight(fighter);
        Appear();
        health.InitHealth();
    }

    public override void FightStarted(){}

    public override void Withdraw()
    {
        Disappear();
    }

    public override void Win()
    {
        ResetMonster();
    }

    public override void Draw() 
    {
        Disappear();
    }
}
