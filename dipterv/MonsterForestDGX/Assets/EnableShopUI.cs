using UnityEngine;
using UnityEngine.XR;

public class EnableShopUI : MonoBehaviour
{
    public UiShower uiShower;
    public XRNode input = XRNode.LeftHand;
    private bool inArea = false;
    private bool inShop = false;

    public GameObject notification;

    public IPressed enableShopInput;

    public void Start()
    {
        enableShopInput = KeyBindingManager.GetInstance().shopCollectButton;
        enableShopInput.SubscribeToPressed(ShowUI);
    }

    private void ShowUI()
    {
        if (inArea && !inShop)
        {
            uiShower.ShowUI(this);
            inShop = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = true;
            enableShopInput.Activate();
            notification.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = false;
            enableShopInput.Deactivate();
            notification.SetActive(false);
        }
    }

    public void ClosedUI()
    {
        inShop = false;
    }
}
