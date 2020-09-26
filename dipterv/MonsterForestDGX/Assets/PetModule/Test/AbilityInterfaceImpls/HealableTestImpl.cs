using UnityEngine;

public class HealableTestImpl : PetParameterTestImpl, IHealable
{
    public bool full = false;

    public void Heal(float amount)
    {
        Debug.Log(nameof(HealableTestImpl) + ": Heal (" + amount  +  ")");
    }

    public bool IsFull()
    {
        Debug.Log(nameof(HealableTestImpl) + ": IsFull (" + full + ")");
        return full;
    }
}
