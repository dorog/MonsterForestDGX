using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AbstractCommand[] commands;

    private int actual = -1;

    private bool isRunning = false;

    private void Start()
    {
        foreach(var command in commands)
        {
            command.Controller = this;
        }
    }

    protected AbstractCommand GetNextCommand()
    {
        actual++;

        if(actual == commands.Length)
        {
            actual = 0;
        }

        return commands[actual];
    }

    public void Step()
    {
        if(!isRunning)
        {
            isRunning = true;

            AbstractCommand cmd = GetNextCommand();
            cmd.Execute();
        }
    }

    protected void ResetController()
    {
        actual = -1;
    }

    public virtual void FinishedTheCommand()
    {
        isRunning = false;
    }
}
