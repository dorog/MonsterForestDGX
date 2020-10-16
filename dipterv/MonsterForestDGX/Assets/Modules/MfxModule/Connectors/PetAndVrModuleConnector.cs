using UnityEngine;

public class PetAndVrModuleConnector : MonoBehaviour
{
    public Player player;
    public PetUiShowerComponent petUiShowerComponent;

    void Start()
    {
        Debug.Log("Connect popup and vr movement");
        //petUiShowerComponent.uiActivated += player.MenuState;
        //petUiShowerComponent.uiDeactivated += player.MenuState;
    }
}
