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

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HideUI();
        }
    }
}
