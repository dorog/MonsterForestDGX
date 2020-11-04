using UnityEngine;

public class UiLocationChanger : TriggerEvent
{
    public Transform menu;
    public Vector3 offset = new Vector3(0, 4, 0);

    public virtual void ChangeLocation()
    {
        menu.position = transform.position + offset;
        menu.rotation = transform.rotation;
    }

    public override void TriggerEnter(Collider other)
    {
        ChangeLocation();
    }

    public override void TriggerExit(Collider other){}
}
