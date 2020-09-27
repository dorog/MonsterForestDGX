using System;

public interface IHealable : IPetParameter
{
    bool IsFull();
    void Heal(float amount);
    void SubscribeToHealEvents(Action activate, Action deactivate);
    void UnsubscribeFromHealEvents(Action activate, Action deactivate);
}
