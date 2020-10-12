using UnityEngine;

public class MfxExperienceConnector : MonoBehaviour
{
    public MagicCircleHandler magicCircleHandler;
    public ExperienceManager experienceManager;
    public DataManager dataManager;

    void Start()
    {
        //dataManager.expChangedEvent += SetExp;

        //battleManager.Draw += DrawFigth;
        //magicCircleHandler.successCastSpellWithCoverage += AddCastXp;
    }
}
