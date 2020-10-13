﻿using System.Collections.Generic;
using UnityEngine;

public class PatternManagerConnector : AbstractConnector, IUiPatternDataHandler
{
    public PatternManager patternManager;

    [Header("Settings")]
    public UiPattern uiPattern;
    public DataManager dataManager;

    [Header("Optional")]
    public ExperienceManager experienceManager;

    private readonly List<IShopUiPattern> recognizeablePatterns = new List<IShopUiPattern>();

    public override void Setup()
    {
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
        for (int i = 0; i < recognizeablePatterns.Count; i++)
        {
            patternDatas[i] = new ShopUiPatternData()
            {
                ShopUiPattern = recognizeablePatterns[i],
                State = recognizeablePatterns[i].GetState()
            };
        }

        return patternDatas;
    }

    private void CreatePatterns(List<BasePatternSpell> BasePaternSpells, List<int> levels, List<IShopUiPattern> SpellPatterns, float extraHeigh = 0)
    {
        Debug.Log(BasePaternSpells.Count);
        Debug.Log(levels.Count);
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
        uiPatternInstance.Init(id, points, basePaternSpell.icon);
        uiPatternInstance.Connect(patternManager, experienceManager);
        uiPatternInstance.ElementType = basePaternSpell.elementType;
        uiPatternInstance.level = level;
        uiPatternInstance.Spells = basePaternSpell.GetSpells();
        uiPatternInstance.RequiredExps = basePaternSpell.GetRequiredExps();

        SpellPatterns.Add(uiPatternInstance);
    }

    public void SavePatternDatas(ShopUiPatternData[] patternDatas)
    {
        throw new System.NotImplementedException();
    }
}
