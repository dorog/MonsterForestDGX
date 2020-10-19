using UnityEngine;

public class PassiveFighter : AiFighter
{
    public Health health;

    [Range(0, 100)]
    public float blockChance = 10f;

    public GameObject[] extraObjects;
    public ParticleSystem[] extraParticles;

    public GameObject root;

    protected override void React()
    {
        float random = Random.Range(1, 101);

        if (random <= blockChance)
        {
            health.SetDamageBlock();
        }
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
        health.DisableHealth();
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
}
