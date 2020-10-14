using System;

public interface IPatternShopUiManager
{
    void LoadData();
    void SubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method);
    void UnsubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method);
    void ChangedPatternData(int id);
    void SubscibeToPattternDataDataChangedEvent(Action<int> method);
    void UnsubscibeFromPatternDataDataChangedEvent(Action<int> method);
}
