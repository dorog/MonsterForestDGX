using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject continueGame;
    public FileDataIO fileDataIO;

    private void Start()
    {
        if(!fileDataIO.HasFile())
        {
            continueGame.SetActive(false);
        }
    }

    public void NewGame()
    {
        fileDataIO.Create();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
