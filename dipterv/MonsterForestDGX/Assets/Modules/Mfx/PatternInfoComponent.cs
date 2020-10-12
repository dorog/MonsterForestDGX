using UnityEngine;

public class PatternInfoComponent : MonoBehaviour
{
    private IPatternUiManager patternManager;
    private UiPatternData[] patternsData;

    private int id = -1;

    public Transform root;

    public void AddPatternManager(IPatternUiManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetPatternData);
        patternManager.SubscibeToPattternDataDataChangedEvent(RefreshPatternData);
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

    public void SelectPattern(int _id)
    {
        if(_id == id)
        {
            root.gameObject.SetActive(!root.gameObject.activeSelf);
        }
        else
        {
            id = _id;
            ClearChild();
            patternsData[id].UiPattern.InstantiateInfo(root);
            root.gameObject.SetActive(true);
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
