using System.Collections.Generic;
using UnityEngine;

public class PatternManagerConnector : AbstractConnector, IMfxPatternDataHandler
{
    public MfxPatternManager patternManager;

    [Header("Settings")]
    public MfxPattern uiPattern;
    public DataManager dataManager;

    [Header("Optional")]
    public ExperienceManager experienceManager;

    private readonly List<MfxPattern> recognizeablePatterns = new List<MfxPattern>();

    public override void Setup()
    {
        patternManager.patternDataHandler = this;
    }

    public override void Load()
    {
        patternManager.LoadData();
    }

    public MfxPatternData[] LoadPatternDatas()
    {
        CreatePatterns(dataManager.GetBasePatterns(), dataManager.GetBasePatternLevels(), recognizeablePatterns);

        MfxPatternData[] patternDatas = new MfxPatternData[recognizeablePatterns.Count];
        for (int i = 0; i < recognizeablePatterns.Count; i++)
        {
            patternDatas[i] = new MfxPatternData()
            {
                Pattern = recognizeablePatterns[i],
                State = recognizeablePatterns[i].GetState()
            };
        }

        return patternDatas;
    }

    private void CreatePatterns(List<BasePatternSpell> BasePaternSpells, List<int> levels, List<MfxPattern> SpellPatterns, float extraHeigh = 0)
    {
        for (int i = 0; i < BasePaternSpells.Count; i++)
        {
            CreateSpellPattern(i, BasePaternSpells[i], levels[i], SpellPatterns, extraHeigh);
        }
    }

    private void CreateSpellPattern(int id, BasePatternSpell basePaternSpell, int level, List<MfxPattern> SpellPatterns, float extraHeigh = 0)
    {
        List<Vector2> points = new List<Vector2>();
        List<SpellPatternPoint> spellPatternPoints = basePaternSpell.SpellPatternPoints.GetPoints();
        for (int i = 0; i < spellPatternPoints.Count; i++)
        {
            points.Add(spellPatternPoints[i].Point);
        }

        MfxPattern uiPatternInstance = Instantiate(uiPattern, transform);
        uiPatternInstance.Init(id, points, basePaternSpell.icon);
        uiPatternInstance.Connect(patternManager, experienceManager);
        uiPatternInstance.ElementType = basePaternSpell.elementType;
        uiPatternInstance.level = level;
        uiPatternInstance.Spells = basePaternSpell.GetSpells();
        uiPatternInstance.RequiredExps = basePaternSpell.GetRequiredExps();

        SpellPatterns.Add(uiPatternInstance);
    }

    public void SavePatternDatas(MfxPatternData[] patternDatas)
    {
        throw new System.NotImplementedException();
    }
}
