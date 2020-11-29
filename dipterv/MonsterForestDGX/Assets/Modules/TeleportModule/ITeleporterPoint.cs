using UnityEngine;

public interface ITeleporterPoint : ITeleportItem
{
    void TeleportTarget(Transform target);
}
