using UnityEngine;
using UnityEngine.UI;

public class PatternShopComponent : MonoBehaviour
{
    public Text quantityText;
    public Transform content;

    public string currency = "EXP";

    private int quantity = 0;

    private IPatternShopUiManager patternManager;
    public ShopUiPatternData[] patternDatas;

    private ICurrencyHandler currencyHandler;

    public void AddPatternManager(IPatternShopUiManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetPatternData);
        patternManager.SubscibeToPattternDataDataChangedEvent(RefreshPatternData);
    }

    public void AddCurrencyHandler(ICurrencyHandler _currencyHandler)
    {
        currencyHandler = _currencyHandler;
        currencyHandler.SubscribeToQuantityValueChanged(SetQuantity);
    }

    private void SetPatternData(ShopUiPatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
        SetupUI();
    }

    private void SetupUI()
    {
        for (int i = 0; i < patternDatas.Length; i++)
        {
            patternDatas[i].ShopUiPattern.InstantiateUiElement(content, quantity);
        }
    }

    private void RefreshPatternData(int id)
    {
        patternDatas[id].ShopUiPattern.RefreshData();
    }

    private void SetQuantity(float newQuantity)
    {
        quantity = Mathf.FloorToInt(newQuantity);

        quantityText.text = quantity + " " + currency;

        if(patternDatas == null)
        {
            Debug.LogWarning("Bug: PatternDatas arent loaded, before the first quantity!");
            return;
        }
        for (int i = 0; i < patternDatas.Length; i++)
        {
            patternDatas[i].ShopUiPattern.RefreshQuantity(quantity);
        }
    }
}
