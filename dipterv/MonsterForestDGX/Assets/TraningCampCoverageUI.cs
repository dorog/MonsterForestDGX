using System.Collections.Generic;
using UnityEngine;

public class TraningCampCoverageUI : MonoBehaviour
{
    public MagicCircleHandler magicCircleHandler;

    public TraningCampCoverageElementUI[] traningCampCoverageElementUIs;

    private SpellManager spellManager;

    private void Start()
    {
        if (spellManager == null)
        {
            spellManager = SpellManager.GetInstance();
        }
    }

    private void OnEnable()
    {
        magicCircleHandler.successCastSpellDelegateEvent += GetSpellCoverage;
        magicCircleHandler.failedCastSpellDelegateEvent += GetSpellCoverage;

        if(spellManager == null)
        {
            spellManager = SpellManager.GetInstance();
        }
        List<bool> states = spellManager.GetAttackSpellsState();

        for(int i = 0; i < states.Count; i++)
        {
            traningCampCoverageElementUIs[i].ResetUI();
            traningCampCoverageElementUIs[i].gameObject.SetActive(states[i]);
        }
    }

    private void OnDisable()
    {
        magicCircleHandler.successCastSpellDelegateEvent -= GetSpellCoverage;
        magicCircleHandler.failedCastSpellDelegateEvent -= GetSpellCoverage;
    }

    private void GetSpellCoverage()
    {
        List<CoverageResult> coverageResults = spellManager.GetCoverageResults();

        for (int i = 0; i < coverageResults.Count; i++)
        {
            traningCampCoverageElementUIs[i].ShowResult(coverageResults[i].Result, coverageResults[i].Min, coverageResults[i].Name);
        }
    }
}
