using System.Collections.Generic;
using UnityEngine;

public class MfxPatternDataHandler : AbstractConnector, ISpellHandler
{
    private readonly List<UiPattern> uiPatterns = new List<UiPattern>();

    [Header("Connector dependencies")]
    public MagicCircleHandler magicCircleHandler;
    public PatternManager patternManager;
    public PatternRecognizerComponent patternRecognizerComponent;
    public MfxTraningCampPatternComponent traningCampPatternComponent;
    public PatternInfoComponent patternInfoComponent;
    public PatternShopComponent patternShopComponent;

    public DataManager dataManager;

    public override void Setup()
    {
        patternRecognizerComponent.AddPatternManager(patternManager);
        traningCampPatternComponent.AddPatternManager(patternManager);
        patternInfoComponent.AddPatternManager(patternManager);

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
