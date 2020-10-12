using System;
using System.Linq;
using UnityEngine;

public class PatternManager : MonoBehaviour, IPatternManager, IPatternUiManager, IPatternShopUiManager
{
    private event Action<PatternData[]> LoadedPatternData;
    private event Action<UiPatternData[]> LoadedUiPatternData;
    private event Action<ShopUiPatternData[]> LoadedShopUiPatternData;

    private event Action<PatternDataDifference> ChangedPatternDataState;
    private event Action<int> ChangedPatternDataData;

    private ShopUiPatternData[] patternDatas;
    public IUiPatternDataHandler patternDataHandler;

    public void LoadData()
    {
        patternDatas = patternDataHandler.LoadPatternDatas();
        LoadedPatternData?.Invoke(patternDatas);
        LoadedUiPatternData?.Invoke(patternDatas.ToList().Select(x => new UiPatternData() { State = x.State, UiPattern = x.ShopUiPattern }).ToArray());
        LoadedShopUiPatternData?.Invoke(patternDatas);
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

    public void SubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method)
    {
        LoadedShopUiPatternData += method;
    }

    public void UnsubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method)
    {
        LoadedShopUiPatternData -= method;
    }
}
