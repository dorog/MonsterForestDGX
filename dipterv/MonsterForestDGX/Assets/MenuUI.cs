using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public Transform menu;
    public GameObject ui;

    public void HideUI()
    {
        ui.SetActive(false);
    }

    public void ShowUI(Vector3 position, Quaternion rotation)
    {
        menu.position = position;
        menu.rotation = rotation;

        ui.SetActive(true);
    }
}
