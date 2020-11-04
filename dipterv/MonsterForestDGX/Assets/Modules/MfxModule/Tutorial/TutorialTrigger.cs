using UnityEngine;

public class TutorialTrigger : TriggerEvent
{
    public Controller controller;

    public override void TriggerEnter(Collider other)
    {
        controller.StartController();
    }

    public override void TriggerExit(Collider other){}
}
