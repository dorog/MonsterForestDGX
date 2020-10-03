using System;
using UnityEngine;

public class MfxPatternCurrencyHandler : MonoBehaviour, ICurrencyHandler
{
    private event Action<float> QuantityChanged;

    private void Start()
    {
        PlayerExperience playerExperience = PlayerExperience.GetInstance();
        playerExperience.ExpChanged += QuantityChangedMethod;
    }

    private void QuantityChangedMethod(float newExpQuantity)
    {
        QuantityChanged?.Invoke(newExpQuantity);
    }

    public void LoadQuantity()
    {
        DataManager dataManager = DataManager.GetInstance();
        float exp = dataManager.GetExp();
        QuantityChanged?.Invoke(exp);
    }

    public void ItemChanged(int id, float price)
    {
        DataManager dataManager = DataManager.GetInstance();
        float quantity = dataManager.LevelUpSpell(id, price);

        QuantityChanged?.Invoke(quantity);
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
