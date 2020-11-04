using UnityEngine;

public class UiShower : UiLocationChanger
{
    public override void ChangeLocation()
    {
        base.ChangeLocation();

        menu.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        menu.gameObject.SetActive(false);
    }

    public override void TriggerExit(Collider other)
    {
        base.TriggerExit(other);
        HideUI();
    }
}
