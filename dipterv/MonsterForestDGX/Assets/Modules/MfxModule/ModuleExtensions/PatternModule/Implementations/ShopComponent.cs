using UnityEngine;
using UnityEngine.UI;

public class ShopComponent : MonoBehaviour
{
    public Text quantityText;
    public Transform content;

    public string currency = "EXP";

    private int quantity = 0;

    private MfxPatternManager patternManager;
    public MfxPatternData[] patternDatas;

    private ICurrencyHandler currencyHandler;

    public PatternUpgraderComponent patternUpgraderComponent;
    public ExperienceManager experienceManager;

    public void AddPatternManager(MfxPatternManager _patternManager)
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

    private void SetPatternData(MfxPatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
        SetupUI();
    }

    private void SetupUI()
    {
        for (int i = 0; i < patternDatas.Length; i++)
        {
            patternDatas[i].Pattern.InstantiateUiElement(content, quantity, this);
        }
    }

    private void RefreshPatternData(int id)
    {
        patternDatas[id].Pattern.RefreshData();
    }

    private void SetQuantity(float newQuantity)
    {
        quantity = Mathf.FloorToInt(newQuantity);

        quantityText.text = quantity + " " + currency;

        for (int i = 0; i < patternDatas.Length; i++)
        {
            patternDatas[i].Pattern.RefreshQuantity(quantity);
        }
    }

    public void BuyOrUpdate(int id)
    {
        int value = patternDatas[id].Pattern.GetRequiredExpValue();
        experienceManager.RemoveExp(value);
        experienceManager.Save();

        patternUpgraderComponent.Increase(id);
    }
}
