using System;
using UnityEngine;

public class PetParameterTestImpl : MonoBehaviour
{
    public event Action Activate;
    public event Action Deactivate;

    private bool activeTurn = true;

    public void Switch()
    {
        if (activeTurn)
        {
            Activate?.Invoke();
        }
        else
        {
            Deactivate?.Invoke();
        }

        activeTurn = !activeTurn;
    }

    public void SubscribeToEvents(Action activate, Action deactivate)
    {
        Debug.Log("SubscribeToEvents");
        Activate += activate;
        Deactivate += deactivate;
    }

    public void UnsubscribeToEvents(Action activate, Action deactivate)
    {
        Debug.Log("UnsubscribeToEvents");
        Activate -= activate;
        Deactivate -= deactivate;
    }
}
