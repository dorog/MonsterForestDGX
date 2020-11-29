using UnityEngine;
using UnityEngine.UI;

public class UpdateablePatternUI : MonoBehaviour
{
    public Image iconImage;
    public Text spellNameText;
    public Text requiredExpValue;
    public Text buttonText;
    public Button button;

    private int id;
    private MfxPattern uiPattern;
    private ShopComponent shopComponent;

    public void Init(ShopComponent _shopComponent, int _id, MfxPattern _uiPattern, int quantity = 0)
    {
        uiPattern = _uiPattern;
        shopComponent = _shopComponent;
        id = _id;

        Refresh(quantity);
    }

    public void Refresh(int quantity)
    {
        RefreshData();
        RefreshQuantity(quantity);
    }

    public void RefreshData()
    {
        iconImage.sprite = uiPattern.GetIcon();
        spellNameText.text = uiPattern.GetElementType().ToString();
        spellNameText.color = uiPattern.GetElementType().GetElementTypeColor();

        requiredExpValue.text = uiPattern.GetRequiredExp();

        if (uiPattern.GetLevelValue() == 0)
        {
            buttonText.text = "Buy";
        }
        else if (uiPattern.IsMaxed())
        {
            button.gameObject.SetActive(false);
            //Exp and value set false too?
        }
        else
        {
            buttonText.text = "Update";
        }
    }

    public void RefreshQuantity(int quantity)
    {
        bool enable = (uiPattern.GetRequiredExpValue() <= quantity);
        button.interactable = enable;
        requiredExpValue.color = enable ? Color.white : Color.red;
    }

    public void BuyOrUpdate()
    {
        shopComponent.BuyOrUpdate(id);
    }

    public void ShowInfo()
    {
        uiPattern.ShowInfo();
    }
}
