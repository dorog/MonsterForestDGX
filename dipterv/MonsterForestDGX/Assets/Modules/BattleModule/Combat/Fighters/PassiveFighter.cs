using UnityEngine;

public class PassiveFighter : AiFighter
{
    public Health health;

    public GameObject[] extraObjects;
    public ParticleSystem[] extraParticles;

    public GameObject root;

    protected override void React()
    {
        health.SetDamageBlock();
    }

    protected override void Appear()
    {
        foreach (var go in extraObjects)
        {
            go.SetActive(true);
        }
    }

    protected override void Disappear()
    {
        health.DisapperHealth();
        DisableExtras();
    }

    protected override void ResetMonster()
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

    public override void Disable()
    {
        root.SetActive(false);
        health.DisableHealth();
    }

    public override void SetupForFight(Fighter fighter)
    {
        base.SetupForFight(fighter);
        health.InitHealth();
    }

    public override void FightStarted(){}
}
