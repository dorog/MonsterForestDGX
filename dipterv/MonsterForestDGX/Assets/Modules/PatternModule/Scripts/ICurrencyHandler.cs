using System;

public interface ICurrencyHandler
{
    void SubscribeToQuantityValueChanged(Action<float> method);
    void UnsubscribeToQuantityValueChanged(Action<float> method);
    void ItemChanged(int id, float price);
}
