using System;
using System.Collections.Generic;
using UnityEngine;

//TODO: Not correct
public class MultiInput : MonoBehaviour, IMultiInput, IPressed
{
    public string id;
    public IPressed[] inputs;

    private int pressedLastIndex = defaultLastIndex;
    private static int defaultLastIndex = -1;

    private event Action PressingAction;
    private event Action PressedAction;
    private event Action ReleasedAction;

    private readonly List<InputIndex> indexes = new List<InputIndex>();

    public void Start()
    {
        //inputs = KeyBindingManager.GetInstance().magicCircleInputs;

        for (int i = 0; i < inputs.Length; i++)
        {
            InputIndex index = new InputIndex(i, this);
            inputs[i].SubscribeToPressed(index.Call);
            inputs[i].SubscribeToPressing(Pressing);
            inputs[i].SubscribeToReleased(Released);
            indexes.Add(index);
        }
    }

    public void Activate()
    {
        foreach(var input in inputs)
        {
            input.Activate();
        }
    }

    public void Deactivate()
    {
        foreach (var input in inputs)
        {
            input.Deactivate();
        }
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
    public void SubscribeToReleased(Action method) => ReleasedAction += method;
    public void UnsubscribeFromReleased(Action method) => ReleasedAction -= method;

    public void Reset()
    {
        pressedLastIndex = defaultLastIndex;
    }

    public void SubscribeToPressed(Action method1, Action method2)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeFromPressed(Action method1, Action method2)
    {
        throw new NotImplementedException();
    }

    public void SubscribeToPressed(Action[] methods)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeFromPressed(Action[] methods)
    {
        throw new NotImplementedException();
    }

    private class InputIndex
    {
        private int _index;
        private MultiInput multiSwitch;
        public InputIndex(int index, MultiInput multiSwitchInput)
        {
            _index = index;
            multiSwitch = multiSwitchInput;
        }

        public void Call()
        {
            multiSwitch.Pressed(_index);
        }
    }
}
