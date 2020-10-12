using System;

public class MfxPatternCurrencyHandler : AbstractConnector, ICurrencyHandler
{
    private event Action<float> QuantityChanged;

    public ExperienceManager experienceManager;
    public DataManager dataManager;
    public PatternShopComponent patternShopComponent;

    public override void Setup()
    {
        experienceManager.ExperienceIO = dataManager;
        experienceManager.SubscribeToExpChanged(QuantityChangedMethod);

        patternShopComponent.AddCurrencyHandler(this);
    }

    public override void Load()
    {
        experienceManager.Load();
    }

    private void QuantityChangedMethod(float newExpQuantity)
    {
        QuantityChanged?.Invoke(newExpQuantity);
    }

    public void ItemChanged(int id, float price)
    {
        dataManager.LevelUpSpell(id);
        experienceManager.RemoveExp(price);
        experienceManager.Save();
    }

    public void SubscribeToQuantityValueChanged(Action<float> method)
    {
        QuantityChanged += method;
    }

    public void UnsubscribeToQuantityValueChanged(Action<float> method)
    {
        QuantityChanged -= method;
    }
}
