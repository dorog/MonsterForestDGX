using System;

public interface IPatternInfoManager
{
    void LoadData();
    void SubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method);
    void UnsubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method);
    void ChangedPatternData(int id);
    void SubscibeToPattternDataDataChangedEvent(Action<int> method);
    void UnsubscibeFromPatternDataDataChangedEvent(Action<int> method);
    void SelectPatternData(int id);
    void SubscibeToPattternDataSelectedEvent(Action<int> method);
    void UnsubscibeFromPatternDataSelectedEvent(Action<int> method);
}
