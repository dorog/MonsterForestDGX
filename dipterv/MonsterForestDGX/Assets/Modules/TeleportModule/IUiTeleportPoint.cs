using UnityEngine;

public interface IUiTeleportPoint : ITeleportItem
{
    void InstantiateUI();
    void ReachedUiLocation();
    void LeftUiLocation();
}
