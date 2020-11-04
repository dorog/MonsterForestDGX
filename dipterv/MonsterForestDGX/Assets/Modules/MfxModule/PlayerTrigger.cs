using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public TriggerEvent[] events;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach(var triggerEvent in events)
            {
                triggerEvent.TriggerEnter(other);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var triggerEvent in events)
            {
                triggerEvent.TriggerExit(other);
            }
        }
    }
}
