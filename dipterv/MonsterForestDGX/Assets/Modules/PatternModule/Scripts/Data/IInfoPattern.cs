using UnityEngine;

public interface IInfoPattern
{
    void InstantiateInfo(Transform root);
    void ChangeVisibility();
    void RefreshInfo();
}
