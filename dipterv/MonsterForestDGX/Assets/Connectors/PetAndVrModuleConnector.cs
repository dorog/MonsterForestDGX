using UnityEngine;

public class PetAndVrModuleConnector : MonoBehaviour
{
    public Player player;
    public PetUiShowerComponent petUiShowerComponent;

    void Start()
    {
        petUiShowerComponent.uiActivated += player.MenuState;
        petUiShowerComponent.uiDeactivated += player.MenuState;
    }
}
