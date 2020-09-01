using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public int id;
    public Teleport teleport;
    public GameObject port;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered: " + id);
        teleport.ReachedTerritory(id);
    }
}
