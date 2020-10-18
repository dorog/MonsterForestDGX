using UnityEngine;

public class MockTarget : MonoBehaviour, ITarget
{
    public void TakeDamage(float dmg, ElementType elementType){}

    public void TakeDamage(float dmg, ElementType elementType, Health health){}
}
