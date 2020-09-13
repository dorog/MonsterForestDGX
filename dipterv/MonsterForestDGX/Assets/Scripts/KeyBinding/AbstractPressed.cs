using UnityEngine;

public abstract class AbstractPressed : MonoBehaviour, IPressed
{
    public string id;
    private bool pressed = false;
    private bool pressing = false;
    private bool setted = false;

    private bool isActive = true;

    public bool IsPressed()
    {
        if (pressed)
        {
            Debug.Log("Pressed...");
            pressed = false;
            return true;
        }

        return false;
    }

    public bool IsPressing()
    {
        return pressing;
    }

    private void Update()
    {
        if (isActive)
        {
            pressing = GetPressState();

            if (pressing && !setted)
            {
                pressed = true;
                setted = true;
            }
            else if (!pressing)
            {
                setted = false;
            }
        }
    }

    protected abstract bool GetPressState();

    private void ResetStates()
    {
        pressed = false;
        pressing = false;
        setted = false;
    }

    public void Activate()
    {
        ResetStates();
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
