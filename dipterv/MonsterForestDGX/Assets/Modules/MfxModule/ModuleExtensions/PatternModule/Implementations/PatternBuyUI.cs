using UnityEngine;
using UnityEngine.UI;

public class PatternBuyUI : MonoBehaviour
{
    public Text spellNameText;
    public Text difficultyValueText;
    public Text valueTitleText;
    public Text valueValueText;
    public Text cooldownValueText;
    public Text levelText;

    private MfxPattern mfxPattern;

    public void Init(MfxPattern _mfxPattern)
    {
        mfxPattern = _mfxPattern;
    }

    public void Refresh()
    {
        spellNameText.text = mfxPattern.GetElementType().ToString();
        spellNameText.color = mfxPattern.GetElementType().GetElementTypeColor();

        difficultyValueText.text = mfxPattern.GetDifficulty()[0];
        difficultyValueText.color = mfxPattern.GetDifficultyColor()[0];
        valueTitleText.text = mfxPattern.GetSpellTypeUI() + ":";
        valueValueText.text = mfxPattern.GetTypeValueUI()[0];
        cooldownValueText.text = mfxPattern.GetCooldownUI()[0];
        levelText.text = mfxPattern.level.ToString();
    }
}
