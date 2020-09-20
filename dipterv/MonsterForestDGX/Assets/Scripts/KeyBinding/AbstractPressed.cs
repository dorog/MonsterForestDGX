using System;
using UnityEngine;

public abstract class AbstractPressed : MonoBehaviour, IPressed
{
    public string id;
    private bool pressing = false;
    private bool setted = false;

    private bool isActive = false;

    private event Action PressingAction;
    private int actionIndex = -1;
    private Action[] actions;
    private event Action PressedAction;
    private event Action ReleasedAction;

    public void Update()
    {
        if (isActive)
        {
            pressing = GetPressState();

            if (pressing)
            {
                if(!setted)
                {
                    PressedAction?.Invoke();
                    setted = true;
                }

                PressingAction?.Invoke();
            }
            else if (!pressing)
            {
                if (setted)
                {
                    ReleasedAction?.Invoke();
                }
                setted = false;
            }
        }
    }

    protected abstract bool GetPressState();

    public void Activate()
    {
        Reset();
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void SubscribeToPressing(Action method)
    {
        PressingAction += method;
    }

    public void SubscribeToPressed(Action method)
    {
        PressedAction += method;
    }

    public void UnsubscribeFromPressing(Action method)
    {
        PressingAction -= method;
    }

    public void UnsubscribeFromPressed(Action method)
    {
        PressedAction -= method;
    }

    public void SubscribeToReleased(Action method)
    {
        ReleasedAction += method;
    }

    public void UnsubscribeFromReleased(Action method)
    {
        ReleasedAction -= method;
    }

    public void Reset()
    {
        pressing = false;
        setted = false;
        actionIndex = -1;
    }

    public void SubscribeToPressed(Action[] methods)
    {
        actions = methods;
        PressedAction += SteppingAction;
    }

    public void UnsubscribeFromPressed(Action[] methods)
    {
        actions = null;
        actionIndex = -1;
        PressedAction -= SteppingAction;
    }

    private void SteppingAction()
    {
        actionIndex++;
        if(actionIndex >= actions.Length)
        {
            actionIndex = 0;
        }

        actions[actionIndex]?.Invoke();
    }
}
