using UnityEngine;

public class PatternInfoComponent : MonoBehaviour
{
    private IPatternUiManager patternManager;
    private UiPatternData[] patternDatas;

    public SpellElementInfoUI spellElementInfoUI;

    private int id = -1;

    public void Init(IPatternUiManager _patternManager)
    {
        patternManager = _patternManager;
        patternManager.SubscibeToPatternDataLoadedEvent(SetPatternData);
        patternManager.SubscibeToPattternDataDataChangedEvent(RefreshPatternData);
    }

    private void SetPatternData(UiPatternData[] _patternDatas)
    {
        patternDatas = _patternDatas;
    }

    private void RefreshPatternData(int changedPatternId)
    {
        if(id == changedPatternId)
        {
            spellElementInfoUI.Refresh();
        }
    }

    public void SelectPattern(int _id)
    {
        if(_id == id)
        {
            if (spellElementInfoUI.IsVisible())
            {
                HideInfo();
            }
            else
            {
                ShowInfo();
            }
        }
        else
        {
            id = _id;
            spellElementInfoUI.SetPattern(patternDatas[id].UiPattern);
            ShowInfo();
        }
    }

    public void ShowInfo()
    {
        spellElementInfoUI.ShowUI();
    }

    public void HideInfo()
    {
        spellElementInfoUI.HideUI();
    }
}
