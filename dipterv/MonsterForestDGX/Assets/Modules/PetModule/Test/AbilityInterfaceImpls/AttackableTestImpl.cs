using System;
using UnityEngine;

public class AttackableTestImpl : PetParameterTestImpl, IAttackable
{
    public void TakeDamageFromPet(float amount)
    {
        Debug.Log(nameof(AttackableTestImpl) + ": Take damage (" + amount + ")");
    }

    public void SubscribeToAttackEvents(Action activate, Action deactivate)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeFromAttackEvents(Action activate, Action deactivate)
    {
        throw new NotImplementedException();
    }
}
