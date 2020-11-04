using UnityEngine;

public class SuccessButton : MonoBehaviour
{
    public Controller controller;

    private void OnEnable()
    {
        controller.StartController();
    }
}
