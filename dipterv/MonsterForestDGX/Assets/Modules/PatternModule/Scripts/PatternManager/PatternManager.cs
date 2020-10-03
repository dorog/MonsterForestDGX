using System;
using UnityEngine;

public class PatternManager : MonoBehaviour, IPatternManager, IPatternUiManager
{
    private event Action<PatternData[]> LoadedPatternData;
    private event Action<UiPatternData[]> LoadedUiPatternData;

    private event Action<PatternDataDifference> ChangedPatternDataState;
    private event Action<int> ChangedPatternDataData;

    private UiPatternData[] patternDatas;
    public IUiPatternDataHandler patternDataHandler;

    public void LoadData()
    {
        patternDatas = patternDataHandler.LoadPatternDatas();
        LoadedPatternData?.Invoke(patternDatas);
        LoadedUiPatternData?.Invoke(patternDatas);
    }

    public void ChangePatternDataState(int id, PatternState patternState)
    {
        PatternState oldState = patternDatas[id].State;
        bool stateChanged = oldState != patternState;

        if (stateChanged)
        {
            patternDatas[id].State = patternState;
            patternDataHandler.SavePatternDatas(patternDatas);

            ChangedPatternDataState?.Invoke(new PatternDataDifference()
            {
                Id = id,
                OldState = oldState,
                NewState = patternState
            });

            ChangedPatternDataData?.Invoke(id);
        }
    }

    public void ChangedPatternData(int id)
    {
        ChangedPatternDataData?.Invoke(id);
    }

    public void SubscibeToPatternDataLoadedEvent(Action<PatternData[]> method)
    {
        LoadedPatternData += method;
    }

    public void UnsubscibeFromPatternDataLoadedEvent(Action<PatternData[]> method)
    {
        LoadedPatternData -= method;
    }

    public void SubscibeToPattternDataStateChangedEvent(Action<PatternDataDifference> method)
    {
        ChangedPatternDataState += method;
    }

    public void UnsubscibeFromPatternDataStateChangedEvent(Action<PatternDataDifference> method)
    {
        ChangedPatternDataState -= method;
    }

    public void SubscibeToPattternDataDataChangedEvent(Action<int> method)
    {
        ChangedPatternDataData += method;
    }

    public void UnsubscibeFromPatternDataDateChangedEvent(Action<int> method)
    {
        ChangedPatternDataData -= method;
    }

    public void SubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method)
    {
        LoadedUiPatternData += method;
    }

    public void UnsubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method)
    {
        LoadedUiPatternData -= method;
    }
}
