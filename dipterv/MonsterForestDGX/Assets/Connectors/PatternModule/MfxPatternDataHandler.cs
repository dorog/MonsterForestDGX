using System.Collections.Generic;
using UnityEngine;

public class MfxPatternDataHandler : MonoBehaviour, IUiPatternDataHandler
{
    private List<UiPattern> recognizeablePatterns = new List<UiPattern>();

    //TODO: Do other way
    public List<UiPattern> uiPatterns = new List<UiPattern>();

    public UiPattern uiPattern;

    public PatternShopComponent patternShopComponent;
    public PatternInfoComponent patternInfo;

    public UiPatternData[] LoadPatternDatas()
    {
        DataManager dataManager = DataManager.GetInstance();

        CreatePatterns(dataManager.GetBasePatterns(), dataManager.GetBasePatternLevels(), recognizeablePatterns);


        Debug.Log("Dont use public property pls");
        UiPatternData[] patternDatas = new UiPatternData[recognizeablePatterns.Count];
        for(int i = 0; i < recognizeablePatterns.Count; i++)
        {
            patternDatas[i] = new UiPatternData()
            {
                UiPattern = recognizeablePatterns[i],
                State = recognizeablePatterns[i].GetState()
            };
        }
        return patternDatas;
    }

    //Create---------------------
    private void CreatePatterns(List<BasePatternSpell> BasePaternSpells, List<int> levels, List<UiPattern> SpellPatterns, float extraHeigh = 0)
    {
        for (int i = 0; i < BasePaternSpells.Count; i++)
        {
            CreateSpellPattern(i, BasePaternSpells[i], levels[i], SpellPatterns, extraHeigh);
        }
    }

    private void CreateSpellPattern(int id, BasePatternSpell basePaternSpell, int level, List<UiPattern> SpellPatterns, float extraHeigh = 0)
    {
        List<Vector2> points = new List<Vector2>();
        List<SpellPatternPoint> spellPatternPoints = basePaternSpell.SpellPatternPoints.GetPoints();
        for (int i = 0; i < spellPatternPoints.Count; i++)
        {
            points.Add(spellPatternPoints[i].Point);
        }

        UiPattern uiPatternInstance = Instantiate(uiPattern, transform);
        uiPatternInstance.Init(id, points, basePaternSpell.icon, patternShopComponent, patternInfo);
        uiPatternInstance.ElementType = basePaternSpell.SpellPatternPoints.attackType;
        uiPatternInstance.level = level;
        uiPatternInstance.Spells = basePaternSpell.GetSpells();
        uiPatternInstance.RequiredExps = basePaternSpell.GetRequiredExps();

        SpellPatterns.Add(uiPatternInstance);
        uiPatterns.Add(uiPatternInstance);
    }
    //------------------------

    public void SavePatternDatas(UiPatternData[] patternDatas)
    {
        throw new System.NotImplementedException();
    }
}
