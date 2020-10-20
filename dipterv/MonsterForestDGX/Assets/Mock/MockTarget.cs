using UnityEngine;

public class MockTarget : MonoBehaviour, ITarget
{
    public void TakeDamage(float dmg){}

    public void TakeDamage(float dmg, Health health){}
}