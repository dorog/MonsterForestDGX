using UnityEngine;

public abstract class TriggerEvent : MonoBehaviour
{
    public abstract void TriggerEnter(Collider other);
    public abstract void TriggerExit(Collider other);
}
