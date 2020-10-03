using UnityEngine;
using UnityEngine.UI;

public class PatternShopComponent : MonoBehaviour
{
    public Text quantityText;
    public Transform content;
    public SpellElementInfoUI SpellElementInfoUI;

    public string currency = " EXP";

    private int quantity = 0;

    private IPatternUiManager patternManager;
    private UiPatternData[] patternDatas;

    public ICurrencyHandler currencyHandler;
    public PatternInfoComponent patternInfo;

    public void Init(IPatternUiManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetPatternData);
        patternManager.SubscibeToPattternDataDataChangedEvent(RefreshPatternData);
        currencyHandler.SubscribeToQuantityValueChanged(SetQuantity);
    }

    private void SetPatternData(UiPatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
        SetupUI();
    }

    private void SetupUI()
    {
        for (int i = 0; i < patternDatas.Length; i++)
        {
            patternDatas[i].UiPattern.InstantiateUiElement(content, quantity);
        }
    }

    private void RefreshPatternData(int id)
    {
        patternDatas[id].UiPattern.RefreshData();
    }

    private void SetQuantity(float newQuantity)
    {
        quantity = Mathf.FloorToInt(newQuantity);

        quantityText.text = quantity + " " + currency;

        for (int i = 0; i < patternDatas.Length; i++)
        {
            patternDatas[i].UiPattern.RefreshQuantity(quantity);
        }
    }

    public void ChangeQuantity(int id, int price)
    {
        currencyHandler.ItemChanged(id, price);
        patternManager.ChangedPatternData(id);
    }
}
