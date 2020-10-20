using UnityEngine;

public class ResistantTarget : MonoBehaviour, ISpellTarget
{
    public Resistant resistant;
    public FighterPart fighterPart;
    public ExperienceManager experienceManager;

    public void TakeDamage(float dmg, ElementType elementType)
    {
        float resistantDmg = resistant.CalculateDmg(dmg, elementType);
        fighterPart.TakeDamage(resistantDmg);
    }

    public void OnCollisionEnter(Collision collision)
    {
        PatternSpell playerSpell = collision.gameObject.GetComponent<PatternSpell>();
        if (playerSpell != null)
        {
            experienceManager.AddExp(ExpType.Hit.GetExp() * playerSpell.coverage);
        }
    }
}
