using System.Collections.Generic;

public class SpellAndPatternConnector : AbstractConnector, ISpellHandler
{
    private readonly List<MfxPattern> mfxPatterns = new List<MfxPattern>();

    public MagicCircleHandler magicCircleHandler;
    public MfxPatternManager patternManager;

    public override void Setup()
    {
        magicCircleHandler.spellHandler = this;

        patternManager.SubscibeToPatternDataLoadedEvent(PatternDataLoaded);
    }

    public override void Load() { }

    private void PatternDataLoaded(MfxPatternData[] patternDatas)
    {
        foreach (var pattern in patternDatas)
        {
            mfxPatterns.Add(pattern.Pattern);
        }
    }

    public SpellData GetSpellData(int index)
    {
        SpellData spellData = new SpellData()
        {
            Spell = mfxPatterns[index].GetSpell(),
            Cooldown = mfxPatterns[index].GetCooldown(),
            ElementType = mfxPatterns[index].GetElementType()
        };

        return spellData;
    }
}
