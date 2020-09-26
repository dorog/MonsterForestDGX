using System;
using UnityEngine;

public class AttackableTestImpl : PetParameterTestImpl, IAttackable
{
    public void TakeDamageFromPet(float amount)
    {
        Debug.Log(nameof(AttackableTestImpl) + ": Take damage (" + amount + ")");
    }
}
