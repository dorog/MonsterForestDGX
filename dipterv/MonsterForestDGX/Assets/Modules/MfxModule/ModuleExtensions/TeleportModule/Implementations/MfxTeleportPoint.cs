using UnityEngine;

public class MfxTeleportPoint : MonoBehaviour, IUiTeleportPoint, ITeleporterPoint, IUnlockableTeleportPoint
{
    public string title;
    public bool available;
    public Transform point;
    public int id;
    public TeleporterComponent teleporter;

    public MfxTeleportUI mfxTeleportUI;
    private GameObject teleportUI;


    public void ChangedUi()
    {
        teleportUI.SetActive(available);
    }

    public void InstantiateUI(Transform parent)
    {
        MfxTeleportUI mfxTeleportUiInstance = Instantiate(mfxTeleportUI, parent);
        mfxTeleportUiInstance.Init(id, title, teleporter);

        teleportUI = mfxTeleportUiInstance.gameObject;
        if (!available)
        {
            teleportUI.SetActive(false);
        }
    }

    public bool IsUnlocked()
    {
        return available;
    }

    public void LeftUiLocation()
    {
        teleportUI.SetActive(available);
    }

    public void ReachedUiLocation()
    {
        teleportUI.SetActive(false);
    }

    public void TeleportTarget(Transform target)
    {
        target.position = point.position;
        target.rotation = point.rotation;
    }
}
