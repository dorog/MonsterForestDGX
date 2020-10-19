using UnityEngine;

public abstract class FighterPart : MonoBehaviour, ITarget
{
    public bool isRegenerating = true;
    public FighterPartCleaner partCleaner;

    public float maxHealth = 1;
    private float currentHealth;

    public DamageModifier damageModifier;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        if (currentHealth - dmg <= 0)
        {
            float remainingDmg = dmg - currentHealth;

            float realDmg = CalculateDamage(remainingDmg);

            FighterTakeDamage(realDmg);
            if (isRegenerating)
            {
                currentHealth = maxHealth;
            }
            else
            {
                partCleaner.CleanUp();
            }
        }
        else
        {
            currentHealth -= dmg;
        }
    }

    protected abstract void FighterTakeDamage(float damage);

    private float CalculateDamage(float dmg)
    {
        return damageModifier.CalculateDamage(dmg);
    }
}
