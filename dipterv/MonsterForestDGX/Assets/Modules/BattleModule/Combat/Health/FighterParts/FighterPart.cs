using UnityEngine;

public abstract class FighterPart : MonoBehaviour, ITarget
{
    public RegenerationType regenerationType = RegenerationType.Nothing;
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
            if (regenerationType == RegenerationType.Regenerating)
            {
                currentHealth = maxHealth;
            }
            else if(regenerationType == RegenerationType.Die)
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
        if(damageModifier != null)
        {
            return damageModifier.CalculateDamage(dmg);
        }

        return dmg;
    }

    public enum RegenerationType
    {
        Regenerating, Die, Nothing
    }
}
