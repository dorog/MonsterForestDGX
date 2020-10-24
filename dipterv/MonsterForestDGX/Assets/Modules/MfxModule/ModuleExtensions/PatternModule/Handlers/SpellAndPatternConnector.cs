using System.Collections.Generic;
using UnityEngine;

public class SpellAndPatternConnector : AbstractConnector, ISpellHandler
{
    private readonly List<MfxPattern> uiPatterns = new List<MfxPattern>();

    [Header("Connector dependencies")]
    public MagicCircleHandler magicCircleHandler;
    public MfxPatternManager patternManager;

    public MfxTraningCampPatternComponent traningCampPatternComponent;

    public override void Setup()
    {

        traningCampPatternComponent.AddPatternManager(patternManager);

        magicCircleHandler.spellHandler = this;
    }

    public override void Load(){}

    public SpellData GetSpellData(int index)
    {
        SpellData spellData = new SpellData()
        {
            Spell = uiPatterns[index].GetSpell(),
            Cooldown = uiPatterns[index].GetCooldown(),
            ElementType = uiPatterns[index].GetElementType()
        };

        return spellData;
    }
}
