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

    private void Start()
    {
        enableShopInput = KeyBindingManager.GetInstance().shopCollectButton;
    }

    void Update()
    {
        if (inArea && !inShop)
        {
            if (enableShopInput.IsPressed())
            {
                uiShower.ShowUI(this);
                inShop = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inArea = true;
            enableShopInput.Activate();
            notification.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
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
