using UnityEngine;

public class Controller : MonoBehaviour
{
    public AbstractCommand[] commands;

    private int actual = -1;

    private bool isRunning = false;

    public bool looping = true;

    public void InitCommands()
    {
        foreach (var command in commands)
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

    public virtual void StartController()
    {
        if(!isRunning)
        {
            isRunning = true;

            AbstractCommand cmd = GetNextCommand();
            if(cmd != null)
            {
                cmd.Execute();
            }
            else
            {
                StopController();
            }
        }
    }

    public virtual void StopController() 
    {
        actual = -1;
    }

    public virtual void FinishedTheCommand()
    {
        isRunning = false;
    }
}
