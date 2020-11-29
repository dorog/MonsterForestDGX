using System.Collections.Generic;
using UnityEngine;

public class MfxTraningCampPatternComponent : MonoBehaviour
{
    private MfxPatternData[] patternDatas;

    private MfxPatternManager patternManager;

    public DataManager dataManager;

    public void AddPatternManager(MfxPatternManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetUp);
    }

    private void SetUp(MfxPatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
    }

    public List<bool> GetAttackSpellsState()
    {
        List<bool> states = new List<bool>();

        foreach (var patternData in patternDatas)
        {
            if (patternData.State != PatternState.Available)
            {
                states.Add(false);
            }
            else
            {
                states.Add(true);
            }
        }

        return states;
    }

    public List<string> GetAttackSpellNames()
    {
        List<string> names = new List<string>();

        foreach (var patternData in patternDatas)
        {
            if (patternData.State == PatternState.Available)
            {
                names.Add(patternData.Pattern.GetElementType().ToString());
            }
        }

        return names;
    }

    public int GetFilteredAttackSpellState(ElementType elementType)
    {
        for (int i = 0; i < patternDatas.Length; i++)
        {
            //TODO: Change this
            if (patternDatas[i].State == PatternState.Available && patternDatas[i].Pattern.GetElementType().ToString() == elementType.ToString())
            {
                return i;
            }
        }

        return -1;
    }

    public PatternSpellData GetSpellPoints(string name)
    {
        var basePatterns = dataManager.GetBasePatterns();

        if (name == "No")
        {
            return null;
        }
        else
        {
            ElementType type = ElementTypeExtensions.GetElementTypeByName(name);

            for (int i = 0; i < basePatterns.Count; i++)
            {
                if (basePatterns[i].elementType == type)
                {
                    return basePatterns[i];
                }
            }

            return null;
        }
    }
}
