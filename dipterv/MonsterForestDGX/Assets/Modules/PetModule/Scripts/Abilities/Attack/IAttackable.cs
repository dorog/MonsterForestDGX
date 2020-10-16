using System;

public interface IAttackable
{
    void TakeDamageFromPet(float amount);
    void SubscribeToAttackEvents(Action activate, Action deactivate);
    void UnsubscribeFromAttackEvents(Action activate, Action deactivate);
}
