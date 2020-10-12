using System.Collections.Generic;
using UnityEngine;

public class MfxPatternDataHandler : AbstractConnector, IUiPatternDataHandler, ISpellHandler
{
    private readonly List<IShopUiPattern> recognizeablePatterns = new List<IShopUiPattern>();
    private readonly List<UiPattern> uiPatterns = new List<UiPattern>();

    public UiPattern uiPattern;

    [Header("Connector dependencies")]
    public MagicCircleHandler magicCircleHandler;
    public PatternManager patternManager;
    public PatternRecognizerComponent patternRecognizerComponent;
    public PatternShopComponent patternShopComponent;
    public MfxTraningCampPatternComponent traningCampPatternComponent;
    public PatternInfoComponent patternInfoComponent;
    //TODO: Remove and add to other class
    public TextCooldownShower cooldownShower;
    public DataManager dataManager;

    public override void Setup()
    {
        patternRecognizerComponent.AddPatternManager(patternManager);
        traningCampPatternComponent.AddPatternManager(patternManager);
        patternInfoComponent.AddPatternManager(patternManager);
        patternShopComponent.AddPatternManager(patternManager);

        magicCircleHandler.spellHandler = this;
        magicCircleHandler.cooldownShower = cooldownShower;
        patternManager.patternDataHandler = this;
    }

    public override void Load()
    {
        patternManager.LoadData();
    }

    public ShopUiPatternData[] LoadPatternDatas()
    {
        CreatePatterns(dataManager.GetBasePatterns(), dataManager.GetBasePatternLevels(), recognizeablePatterns);

        ShopUiPatternData[] patternDatas = new ShopUiPatternData[recognizeablePatterns.Count];
        for(int i = 0; i < recognizeablePatterns.Count; i++)
        {
            patternDatas[i] = new ShopUiPatternData()
            {
                ShopUiPattern = recognizeablePatterns[i],
                State = recognizeablePatterns[i].GetState()
            };
        }

        return patternDatas;
    }

    //Create---------------------
    private void CreatePatterns(List<BasePatternSpell> BasePaternSpells, List<int> levels, List<IShopUiPattern> SpellPatterns, float extraHeigh = 0)
    {
        for (int i = 0; i < BasePaternSpells.Count; i++)
        {
            CreateSpellPattern(i, BasePaternSpells[i], levels[i], SpellPatterns, extraHeigh);
        }
    }

    private void CreateSpellPattern(int id, BasePatternSpell basePaternSpell, int level, List<IShopUiPattern> SpellPatterns, float extraHeigh = 0)
    {
        List<Vector2> points = new List<Vector2>();
        List<SpellPatternPoint> spellPatternPoints = basePaternSpell.SpellPatternPoints.GetPoints();
        for (int i = 0; i < spellPatternPoints.Count; i++)
        {
            points.Add(spellPatternPoints[i].Point);
        }

        UiPattern uiPatternInstance = Instantiate(uiPattern, transform);
        uiPatternInstance.Init(id, points, basePaternSpell.icon, patternShopComponent, patternInfoComponent);
        uiPatternInstance.ElementType = basePaternSpell.elementType;
        uiPatternInstance.level = level;
        uiPatternInstance.Spells = basePaternSpell.GetSpells();
        uiPatternInstance.RequiredExps = basePaternSpell.GetRequiredExps();

        SpellPatterns.Add(uiPatternInstance);
        uiPatterns.Add(uiPatternInstance);
    }
    //------------------------

    public void SavePatternDatas(ShopUiPatternData[] patternDatas)
    {
        throw new System.NotImplementedException();
    }

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
