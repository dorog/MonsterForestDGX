using UnityEngine;

public interface IUiPattern : IPattern
{
    Sprite GetIcon();
    string GetName();
    void InstantiateUiElement(Transform root, int quantity);
    void InstantiateInfo(Transform root);
    void RefreshInfo();
}
