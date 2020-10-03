using System.Collections.Generic;
using UnityEngine;

public class TraningCampCoverageUI : MonoBehaviour
{
    public TraningCampCoverageElementUI[] traningCampCoverageElementUIs;

    public MfxTraningCampPatternComponent traningCampPatternComponent;

    public PatternRecognizerComponent patternRecognizer;

    private void OnEnable()
    {
        patternRecognizer.Recognize += GetSpellCoverage;

        List<bool> states = traningCampPatternComponent.GetAttackSpellsState();

        for(int i = 0; i < states.Count; i++)
        {
            traningCampCoverageElementUIs[i].ResetUI();
            traningCampCoverageElementUIs[i].gameObject.SetActive(states[i]);
        }
    }

    private void OnDisable()
    {
        patternRecognizer.Recognize -= GetSpellCoverage;
    }

    private void GetSpellCoverage(List<CoverageResult> coverageResults)
    {
        Debug.Log("Comment");
        /*for (int i = 0; i < coverageResults.Count; i++)
        {
            traningCampCoverageElementUIs[i].ShowResult(coverageResults[i].Result, coverageResults[i].Min, coverageResults[i].Name);
        }*/
    }

    public void FilterSpellCoverage(ElementType elementType)
    {
        int chosed = traningCampPatternComponent.GetFilteredAttackSpellState(elementType);

        for (int i = 0; i < traningCampCoverageElementUIs.Length; i++)
        {
            traningCampCoverageElementUIs[i].ResetUI();
            traningCampCoverageElementUIs[i].gameObject.SetActive(false);
        }

        traningCampCoverageElementUIs[chosed].gameObject.SetActive(true);  
    }
}
