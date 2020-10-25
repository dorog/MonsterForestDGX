using UnityEngine;

public class UiShower : MonoBehaviour
{
    public Transform menu;
    public Vector3 offset = new Vector3(0, 4, 0);

    private void ShowUI()
    {
        menu.position = transform.position + offset;
        menu.rotation = transform.rotation;

        menu.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        menu.gameObject.SetActive(false);
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
