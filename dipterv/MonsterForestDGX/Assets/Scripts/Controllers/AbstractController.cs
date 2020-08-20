using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractController : MonoBehaviour
{
    public Command[] commands;

    private int actual = -1;

    public delegate void FinishedCommand();
    public FinishedCommand finishedCommandEvent;

    private void Start()
    {
        foreach(var command in commands)
        {
            command.Controller = this;
        }
    }

    protected Command GetNextCommand()
    {
        actual++;

        if(actual == commands.Length)
        {
            actual = 0;
        }

        return commands[actual];
    }
}
