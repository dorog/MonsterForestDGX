using UnityEngine;

public class BridgePuzzle : MonoBehaviour
{
    //public ElementMovement[] elements;
    public float differentTime = 1f;

    public void DisappearContinously()
    {
        /*for(int i = 0; i < elements.Length; i++)
        {
            elements[i].OpenContinously(differentTime * i);
        }*/
    }

    public void DisappearInstantly()
    {
        /*for (int i = 0; i < elements.Length; i++)
        {
            elements[i].DisappearInstantly();
        }*/
    }
}
