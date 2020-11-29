using UnityEngine;

public class PatternUpgraderComponent : MonoBehaviour
{
    private IPatternShopUiManager patternManager;
    private ShopUiPatternData[] patternDatas;

    public void AddPatternManager(IPatternShopUiManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetPatternData);
    }

    private void SetPatternData(ShopUiPatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
    }

    public void Increase(int id)
    {
        if(patternDatas[id].State != PatternState.Maxed)
        {
            patternDatas[id].Pattern.Increase();
        }
    }
}
