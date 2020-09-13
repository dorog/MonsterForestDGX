using System.Collections.Generic;
using UnityEngine;

public class MultiSwitchInput : MonoBehaviour, IMultiInput, IPressed
{
    public string id;
    public IPressed[] inputs;

    private int lastIndex = defaultLastIndex;
    private readonly static int defaultLastIndex = -1;

    private readonly List<int> indexes = new List<int>();

    private void Start()
    {
        inputs = KeyBindingManager.GetInstance().magicCircleInputs;
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

    public bool IsPressed()
    {
        indexes.Clear();

        int index = defaultLastIndex;
        for (int i = 0; i < inputs.Length; i++)
        {
            if (inputs[i].IsPressed())
            {
                index = i;
                indexes.Add(i);
            }
        }

        if(indexes.Count == 0)
        {
            return false;
        }
        else if(indexes.Count == 1)
        {
            if(lastIndex != index)
            {
                lastIndex = index;
                return true;
            }

            return false;
        }
        else
        {
            indexes.Remove(lastIndex);
            lastIndex = indexes[0];
            return true;
        }
    }

    public bool IsPressing()
    {
        //TODO: Copy the IsPressed
        int count = 0;
        foreach (var input in inputs)
        {
            if (input.IsPressing())
            {
                count++;
            }
        }

        return count >= 0;
    }
}
