using UnityEngine;

public class InputTester : MonoBehaviour
{
    public IPressed pressInput;

    private void Start()
    {
        pressInput = GetComponent<IPressed>();
    }

    void Update()
    {
        /*if (pressInput.IsPressing())
        {
            Debug.Log("Pressing...");
        }
        if (pressInput.IsPressed())
        {
            Debug.Log("Pressed");
        }*/
    }
}
