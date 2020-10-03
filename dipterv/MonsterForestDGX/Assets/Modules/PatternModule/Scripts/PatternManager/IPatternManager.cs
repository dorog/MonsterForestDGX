using System;

public interface IPatternManager
{
    void LoadData();
    void ChangePatternDataState(int id, PatternState patternState);
    void SubscibeToPatternDataLoadedEvent(Action<PatternData[]> method);
    void UnsubscibeFromPatternDataLoadedEvent(Action<PatternData[]> method);
    void SubscibeToPattternDataStateChangedEvent(Action<PatternDataDifference> method);
    void UnsubscibeFromPatternDataStateChangedEvent(Action<PatternDataDifference> method);
}
