using UnityEngine;

public class FighterPartCleaner : MonoBehaviour
{
    public GameObject fighterPart;

    public void CleanUp()
    {
        fighterPart.SetActive(false);
    }
}
