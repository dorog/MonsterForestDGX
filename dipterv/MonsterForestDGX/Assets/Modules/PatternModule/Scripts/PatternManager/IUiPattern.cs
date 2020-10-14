using UnityEngine;

public interface IUiPattern : IPattern
{
    void InstantiateInfo(Transform root);
    void ChangeVisibility();
    void RefreshInfo();
    string GetName();
}
