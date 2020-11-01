using UnityEngine;

public class UiLocationChanger : MonoBehaviour
{
    public Transform menu;
    public Vector3 offset = new Vector3(0, 4, 0);

    public virtual void ChangeLocation()
    {
        menu.position = transform.position + offset;
        menu.rotation = transform.rotation;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeLocation();
        }
    }
}
