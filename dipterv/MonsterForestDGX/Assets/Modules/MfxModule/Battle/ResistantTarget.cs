
public class ResistantTarget : FighterPart, ISpellTarget
{
    public Resistant resistant;

    public void TakeDamage(float dmg, ElementType elementType)
    {
        float resistantDmg = resistant.CalculateDmg(dmg, elementType);
        TakeDamage(resistantDmg);
    }
}
