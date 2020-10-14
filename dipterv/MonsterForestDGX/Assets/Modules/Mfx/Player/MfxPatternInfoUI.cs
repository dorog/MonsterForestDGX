using UnityEngine;

public class MfxPatternInfoUI : MonoBehaviour
{
    private MfxPattern spellPattern = null;

    public PatternBuyUI patternBuyUI;
    public PatternUpdateUI patternUpdateUI;

    public void SetPattern(MfxPattern spellPattern)
    {
        this.spellPattern = spellPattern;
        patternBuyUI.Init(spellPattern);
        patternUpdateUI.Init(spellPattern);
        Refresh();
    }

    public void Refresh()
    {
        if (spellPattern.IsMaxed() || spellPattern.GetLevelValue() == 0)
        {
            patternBuyUI.Refresh();

            patternBuyUI.gameObject.SetActive(true);
            patternUpdateUI.gameObject.SetActive(false);
        }
        else
        {
            patternUpdateUI.Refresh();

            patternBuyUI.gameObject.SetActive(false);
            patternUpdateUI.gameObject.SetActive(true);
        }
    }
}
