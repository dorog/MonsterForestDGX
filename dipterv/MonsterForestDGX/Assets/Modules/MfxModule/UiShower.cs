using UnityEngine;

public class UiShower : MonoBehaviour
{
    public MenuUI menuUI;
    public Vector3 offset = new Vector3(0, 4, 0);

    public void ShowUI()
    {
        menuUI.gameObject.SetActive(true);
        menuUI.ShowUI(transform.position + offset, transform.rotation);
    }

    public void HideUI()
    {
        menuUI.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowUI();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HideUI();
        }
    }
}
