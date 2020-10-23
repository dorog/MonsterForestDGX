using UnityEngine;

public interface IUiTeleportPoint
{
    void InstantiateUI(Transform parent);
    void ChangedUi();
    void ReachedUiLocation();
    void LeftUiLocation();
}
