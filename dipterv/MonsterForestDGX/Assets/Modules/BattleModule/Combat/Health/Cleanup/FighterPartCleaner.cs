using UnityEngine;

public class FighterPartCleaner : MonoBehaviour
{
    public GameObject fighterPart;
    public GameObject effect;
    public Animator animator;
    public string disapperAnimation;

    public void CleanUp()
    {
        if(fighterPart != null)
        {
            fighterPart.SetActive(false);
        }
        if(effect != null)
        {
            fighterPart.SetActive(true);
        }
        if (animator != null)
        {
            animator.SetTrigger(disapperAnimation);
        }
    }
}
