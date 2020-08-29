using UnityEngine;

public class Controller : MonoBehaviour
{
    public AbstractCommand[] commands;

    private int actual = -1;

    private bool isRunning = false;

    public bool looping = true;

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
            if (looping)
            {
                actual = 0;
                return commands[actual];
            }
            else
            {
                return null;
            }
        }

        return commands[actual];
    }

    public void Step()
    {
        if(!isRunning)
        {
            isRunning = true;

            AbstractCommand cmd = GetNextCommand();
            if(cmd != null)
            {
                cmd.Execute();
            }
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
