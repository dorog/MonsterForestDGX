using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Player player;
    public MovingHandler movingHandler;
    public DataManager dataManager;
    public MfxTeleportPoint baseTeleportPoint;
    public Controller controller;
    public Button runButton;

    public void StartTutorial()
    {
        runButton.enabled = false;
        controller.StartController();
    }

    public void EndTutorial()
    {
        movingHandler.EnableMovement();
        dataManager.TutorialFinished();
        baseTeleportPoint.TeleportTarget(player.transform);
        runButton.enabled = true;
    }
}
