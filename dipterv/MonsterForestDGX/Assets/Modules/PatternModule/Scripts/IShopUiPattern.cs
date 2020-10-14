using UnityEngine;

public interface IShopUiPattern : IPattern
{
    Sprite GetIcon();
    void RefreshQuantity(int quantity);
    void RefreshData();
    void InstantiateUiElement(Transform root, int quantity);
}
