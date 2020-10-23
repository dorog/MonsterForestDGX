using UnityEngine;
using UnityEngine.UI;

public class MfxTeleportUI : MonoBehaviour
{
    public int id;
    public TeleporterComponent teleporter;
    public Text title;

    public void Init(int _id, string _title, TeleporterComponent _teleporter)
    {
        id = _id;
        title.text = _title;
        teleporter = _teleporter;
    }

    public void Teleport()
    {
        teleporter.TeleportTarget(id);
    }
}
