using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = nameof(PatternSpellData), menuName = nameof(PatternSpellData))]
public class PatternSpellData : ScriptableObject
{
    public Sprite icon;
    public ElementType elementType = ElementType.TrueDamage;
    public SpellPatternPoints SpellPatternPoints;
    public PatternSpellLevel[] levelsSpell;

    public PatternSpell[] GetSpells()
    {
        PatternSpell[] spells = new PatternSpell[levelsSpell.Length];

        for (int i = 0; i < levelsSpell.Length; i++)
        {
            spells[i] = levelsSpell[i].playerSpell;
        }

        return spells;
    }

    public int[] GetRequiredExps()
    {
        int[] exps = new int[levelsSpell.Length];

        for (int i = 0; i < levelsSpell.Length; i++)
        {
            exps[i] = levelsSpell[i].exp;
        }

        return exps;
    }
}
