using UnityEngine;

public class MockSpellTarget : MonoBehaviour, ISpellTarget
{
    public float damage = 0f;
    public string elementTypeName = "";

    public void TakeDamage(float dmg, ElementType elementType)
    {
        damage = dmg;
        elementTypeName = elementType.ToString();
    }
}
