using System;

public class MfxPatternCurrencyHandler : AbstractConnector, ICurrencyHandler
{
    private event Action<float> QuantityChanged;

    public ExperienceManager experienceManager;
    public ShopComponent patternShopComponent;

    public override void Setup()
    {
        experienceManager.ExpChanged += QuantityChangedMethod;

        patternShopComponent.AddCurrencyHandler(this);
    }

    public override void Load(){}

    private void QuantityChangedMethod(float newExpQuantity)
    {
        QuantityChanged?.Invoke(newExpQuantity);
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
