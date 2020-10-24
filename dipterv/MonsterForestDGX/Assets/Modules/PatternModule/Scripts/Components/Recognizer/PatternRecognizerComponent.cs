using System;
using System.Collections.Generic;
using UnityEngine;

public class PatternRecognizerComponent : MonoBehaviour
{
    private readonly List<RecognizeablePattern> recognizeablePatterns = new List<RecognizeablePattern>();
    private readonly int defaultUnchangedValue = -1;

    public event Action<List<CoverageResult>> Recognize;

    private IPatternManager patternManager;
    private PatternData[] patternDatas;

    public virtual void AddPatternManager(IPatternManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetPatternData);
        patternManager.SubscibeToPattternDataStateChangedEvent(RefreshRecognizeablePatterns);
    }

    private void SetPatternData(PatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
        SetupRecognizeablePatterns();
    }

    private void SetupRecognizeablePatterns()
    {
        for(int i = 0; i < patternDatas.Length; i++)
        {
            PatternState patternState = patternDatas[i].GetPattern().GetState();
            if (patternState == PatternState.Available)
            {
                recognizeablePatterns.Add(new RecognizeablePattern() 
                { 
                    Id = i,
                    Pattern = patternDatas[i].GetPattern()
                });
            }
        }
    }

    private void RefreshRecognizeablePatterns(PatternDataDifference patternDataDifference)
    {
        if (patternDataDifference.NewState == PatternState.Available && patternDataDifference.OldState != PatternState.Available)
        {
            recognizeablePatterns.Add(new RecognizeablePattern()
            {
                Id = patternDataDifference.Id,
                Pattern = patternDatas[patternDataDifference.Id].GetPattern()
            });
        }
        else if (patternDataDifference.NewState != PatternState.Available && patternDataDifference.OldState == PatternState.Available)
        {
            recognizeablePatterns.RemoveAll(x => patternDataDifference.Id == x.Id);
        }
    }

    public void Guess(Vector2 point)
    {
        foreach (var spellPattern in recognizeablePatterns)
        {
            spellPattern.Pattern.Guess(point);
        }
    }

    public RecognizingResult GetResult()
    {
        List<CoverageResult> coverageResults = new List<CoverageResult>();

        int index = defaultUnchangedValue;
        float max = defaultUnchangedValue;
        for (int i = 0; i < recognizeablePatterns.Count; i++)
        {
            float result = recognizeablePatterns[i].Pattern.GetResult();
            float minCoverage = recognizeablePatterns[i].Pattern.GetMinCoverage();

            coverageResults.Add(new CoverageResult(recognizeablePatterns[i].Id, result, minCoverage));

            if ((minCoverage <= result) && (result > max))
            {
                index = recognizeablePatterns[i].Id;
                max = result;
            }
        }

        Recognize?.Invoke(coverageResults);

        if (index != defaultUnchangedValue)
        {
            RecognizingResult spellResult = new RecognizingResult
            {
                Index = index,
                Coverage = max,
            };

            return spellResult;
        }
        else
        {
            return null;
        }
    }

    public void ResetSpells()
    {
        foreach (var spellPattern in recognizeablePatterns)
        {
            spellPattern.Pattern.ResetPattern();
        }
    }
}
