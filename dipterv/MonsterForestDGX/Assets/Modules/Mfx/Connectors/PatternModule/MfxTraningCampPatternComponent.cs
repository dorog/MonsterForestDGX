using System.Collections.Generic;

public class MfxTraningCampPatternComponent : PatternComponent
{
    private UiPatternData[] patternDatas;

    private IPatternUiManager patternManager;

    public DataManager dataManager;

    public void AddPatternManager(IPatternUiManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetUp);
    }

    private void SetUp(UiPatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
    }

    public List<bool> GetAttackSpellsState()
    {
        List<bool> states = new List<bool>();

        foreach (var patternData in patternDatas)
        {
            if (patternData.GetPattern().GetState() != PatternState.Available)
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
            if (patternData.GetPattern().GetState() == PatternState.Available)
            {
                names.Add(patternData.UiPattern.GetName());
            }
        }

        return names;
    }

    public int GetFilteredAttackSpellState(ElementType elementType)
    {
        for (int i = 0; i < patternDatas.Length; i++)
        {
            //TODO: Change this
            if (patternDatas[i].UiPattern.GetState() == PatternState.Available && patternDatas[i].UiPattern.GetName() == elementType.ToString())
            {
                return i;
            }
        }

        return -1;
    }

    public BasePatternSpell GetSpellPoints(string name)
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
