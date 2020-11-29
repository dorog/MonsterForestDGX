using UnityEngine;

public class PatternInfoComponent : MonoBehaviour
{
    private IPatternInfoManager patternManager;
    private UiPatternData[] patternsData;

    private int id = -1;

    public Transform root;

    public void AddPatternManager(IPatternInfoManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetPatternData);
        patternManager.SubscibeToPattternDataDataChangedEvent(RefreshPatternData);
        patternManager.SubscibeToPattternDataStateChangedEvent(PatternStateChanged);
        patternManager.SubscibeToPattternDataSelectedEvent(SelectPattern);
    }

    private void SetPatternData(UiPatternData[] _patternsData)
    {
        patternsData = _patternsData;
    }

    private void RefreshPatternData(int changedPatternId)
    {
        if(id == changedPatternId)
        {
            patternsData[id].UiPattern.RefreshInfo();
        }
    }

    private void PatternStateChanged(PatternDataDifference patternDataDifference)
    {
        if (id == patternDataDifference.Id)
        {
            if (patternDataDifference.NewState == PatternState.Unavailable)
            {
                patternsData[id].UiPattern.ChangeVisibility();
            }
        }
    }

    public void SelectPattern(int _id)
    {
        if(patternsData[id].State != PatternState.Unavailable)
        {
            if (_id == id)
            {
                patternsData[id].UiPattern.ChangeVisibility();
            }
            else
            {
                id = _id;
                ClearChild();
                patternsData[id].UiPattern.InstantiateInfo(root);
            }
        }
    }

    private void ClearChild()
    {
        if(root.childCount > 0)
        {
            Destroy(root.GetChild(0).gameObject);
        }
    }
}
