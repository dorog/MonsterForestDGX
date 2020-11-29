using UnityEngine;

public class ChestReward : MonoBehaviour
{
    public int id = 0;
    public float angle = 45;
    public KeyBindingManager keyBindingManager;

    private IPressed pressed;

    private bool inRange = false;
    public DataManager dataManager;

    private void OnEnable()
    {
        pressed = keyBindingManager.itemCollectButton;
        pressed.SubscribeToPressed(PickUpChest);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void PickUpChest()
    {
        if (inRange)
        {
            dataManager.RewardStateChanged(id, RewardState.Picked);
            gameObject.SetActive(false);
        }
    }
}
