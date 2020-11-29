using System;
using System.Linq;
using UnityEngine;

public class MfxPatternManager : MonoBehaviour, IPatternManager, IPatternInfoManager, IPatternShopUiManager
{
    private event Action<PatternData[]> LoadedPatternData;
    private event Action<UiPatternData[]> LoadedUiPatternData;
    private event Action<ShopUiPatternData[]> LoadedShopUiPatternData;

    private event Action<MfxPatternData[]> LoadedMfxPatternData;

    private event Action<PatternDataDifference> ChangedPatternDataState;
    private event Action<int> ChangedPatternDataData;
    private event Action<int> SelectedPatternDataData;

    private MfxPatternData[] patternDatas;
    public IMfxPatternDataHandler patternDataHandler;

    public void LoadData()
    {
        patternDatas = patternDataHandler.LoadPatternDatas();

        LoadedMfxPatternData?.Invoke(patternDatas);
        LoadedPatternData?.Invoke(patternDatas.ToList().Select(x => new PatternData() { State = x.State, Pattern = x.Pattern }).ToArray());
        LoadedUiPatternData?.Invoke(patternDatas.ToList().Select(x => new UiPatternData() { State = x.State, UiPattern = x.Pattern }).ToArray());
        LoadedShopUiPatternData?.Invoke(patternDatas.ToList().Select(x => new ShopUiPatternData() { State = x.State, Pattern = x.Pattern }).ToArray());
    }

    //TODO: Not necessary code? Unlock function?
    public void ChangePatternDataState(int id, PatternState patternState)
    {
        PatternState oldState = patternDatas[id].State;
        bool stateChanged = oldState != patternState;

        if (stateChanged)
        {
            patternDatas[id].State = patternState;

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

    public void SubscibeToPatternDataLoadedEvent(Action<MfxPatternData[]> method)
    {
        LoadedMfxPatternData += method;
    }

    public void UnsubscibeFromPatternDataLoadedEvent(Action<MfxPatternData[]> method)
    {
        LoadedMfxPatternData -= method;
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

    public void UnsubscibeFromPatternDataDataChangedEvent(Action<int> method)
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

    public void SubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method)
    {
        LoadedShopUiPatternData += method;
    }

    public void UnsubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method)
    {
        LoadedShopUiPatternData -= method;
    }

    public void SelectPatternData(int id)
    {
        SelectedPatternDataData?.Invoke(id);
    }

    public void SubscibeToPattternDataSelectedEvent(Action<int> method)
    {
        SelectedPatternDataData += method;
    }

    public void UnsubscibeFromPatternDataSelectedEvent(Action<int> method)
    {
        SelectedPatternDataData -= method;
    }
}
