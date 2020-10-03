using System;

public interface IPatternUiManager
{
    void LoadData();
    void SubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method);
    void UnsubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method);
    void ChangedPatternData(int id);
    void SubscibeToPattternDataDataChangedEvent(Action<int> method);
    void UnsubscibeFromPatternDataDateChangedEvent(Action<int> method);
}
