
public class ResistantTarget : ISpellTarget
{
    public Resistant resistant;
    public FighterPart fighterPart;

    public void TakeDamage(float dmg, ElementType elementType)
    {
        float resistantDmg = resistant.CalculateDmg(dmg, elementType);
        fighterPart.TakeDamage(resistantDmg);
    }
}
