using System;

public interface IPatternShopUiManager
{
    void LoadData();
    void SubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method);
    void UnsubscibeToPatternDataLoadedEvent(Action<ShopUiPatternData[]> method);
}
