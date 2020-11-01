using System.Collections.Generic;
using UnityEngine;

public class SpellAndPatternConnector : AbstractConnector, ISpellHandler
{
    private readonly List<MfxPattern> mfxPatterns = new List<MfxPattern>();

    [Header("Connector dependencies")]
    public MagicCircleHandler magicCircleHandler;
    public MfxPatternManager patternManager;

    public MfxTraningCampPatternComponent traningCampPatternComponent;

    public override void Setup()
    {
        Debug.Log("Commented out!");
        //traningCampPatternComponent.AddPatternManager(patternManager);

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
