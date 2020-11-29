using UnityEngine;

public class MfxTeleportPoint : MonoBehaviour, IUiTeleportPoint, ITeleporterPoint
{
    public string title;
    public Transform point;
    public int id;
    public TeleporterComponent teleporter;

    public MfxTeleportUI mfxTeleportUI;
    private GameObject teleportUI;

    private bool available;

    public Transform parent;

    public bool GetState()
    {
        return available;
    }

    public void InstantiateUI()
    {
        MfxTeleportUI mfxTeleportUiInstance = Instantiate(mfxTeleportUI, parent);
        mfxTeleportUiInstance.Init(id, title, teleporter);

        teleportUI = mfxTeleportUiInstance.gameObject;
        if (!available)
        {
            teleportUI.SetActive(false);
        }
    }

    public void LeftUiLocation()
    {
        teleportUI.SetActive(available);
    }

    public void ReachedUiLocation()
    {
        teleportUI.SetActive(false);
    }

    public void SetState(bool available)
    {
        this.available = available;
        teleportUI.SetActive(available);
    }

    public void TeleportTarget(Transform target)
    {
        target.position = point.position;
        target.rotation = point.rotation;
    }
}
