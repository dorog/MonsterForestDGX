using UnityEngine;

public class ExperienceConnector : MonoBehaviour
{
    public MagicCircleHandler magicCircleHandler;
    public ExperienceManager experienceManager;

    public BattleManager battleManager;

    public void Start()
    {
        magicCircleHandler.SuccessCastSpellWithCoverage += AddCastExp;
        battleManager.BlueFighterWon += AddKillExp;
        battleManager.BlueFighterWon += experienceManager.Save;
        battleManager.Draw += experienceManager.Save;
        battleManager.RedFighterWon += experienceManager.ResetExp;
    }

    private void AddCastExp(float coverage)
    {
        experienceManager.AddExp(coverage * ExpType.Cast.GetExp());
    }

    private void AddKillExp()
    {
        Debug.Log("Add Last Hit Coverage / Remove Unity using with this");
        experienceManager.AddExp(1 * ExpType.Kill.GetExp());
    }
}
