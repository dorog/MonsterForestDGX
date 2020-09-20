using System;
using UnityEngine;

//TODO: Add And / Or option
public class SwitchInput : MonoBehaviour, IMultiInput, IPressed
{
    public string id;
    public IPressed positive;
    public IPressed negativ;

    private int pressedLastIndex = defaultLastIndex;
    private static int defaultLastIndex = -1;

    private event Action PressingAction;
    private event Action PressedAction;
    private event Action ReleasedAction;

    public void Start()
    {
        
    }

    public void Activate()
    {
        positive.Activate(); 
        negativ.Activate();
    }

    public void Deactivate()
    {
        positive.Deactivate();
        negativ.Deactivate();
    }

    public void Pressed(int index)
    {
        if (pressedLastIndex != index)
        {
            pressedLastIndex = index;
            PressedAction?.Invoke();
        }
    }

    public void Pressing()
    {
        PressingAction?.Invoke();
    }

    public void Released()
    {
        ReleasedAction?.Invoke();
    }

    public void SubscribeToPressing(Action method) => PressingAction += method;
    public void UnsubscribeFromPressing(Action method) => PressingAction -= method;
    public void SubscribeToPressed(Action method) => PressedAction += method;
    public void UnsubscribeFromPressed(Action method) => PressedAction -= method;
    public void SubscribeToReleased(Action method) =>   ReleasedAction += method;
    public void UnsubscribeFromReleased(Action method) => ReleasedAction -= method;

    public void SubscribeToPressed(Action method1, Action method2)
    {
        
    }

    public void UnsubscribeFromPressed(Action method1, Action method2)
    {
        
    }

    public void SubscribeToPressed(Action[] methods)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeFromPressed(Action[] methods)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }
}
