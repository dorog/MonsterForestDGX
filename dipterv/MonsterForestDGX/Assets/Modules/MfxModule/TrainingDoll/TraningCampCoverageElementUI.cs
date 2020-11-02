using UnityEngine;
using UnityEngine.UI;

public class TraningCampCoverageElementUI : MonoBehaviour
{
    public Text coverageText;
    public Text spellNameText;

    public void ShowResult(float coverage, float minCoverage)
    {
        coverageText.text = coverage.ToString("0") + "/" + minCoverage.ToString("0") + " (%)";
    }

    public void ResetUI()
    {
        coverageText.text = "";
    }
}
