using UnityEngine;
using UnityEngine.UI;

public class PatternUpdateUI : MonoBehaviour
{
    public Text spellNameTextUpdate;

    public Text difficultyValueTextActual;
    public Text difficultyValueTextNext;

    public Text valueTitleTextUpdate;

    public Text valueValueTextActual;
    public Text valueValueTextNext;

    public Text cooldownValueTextActual;
    public Text cooldownValueTextNext;

    public Text levelTextActual;
    public Text levelTextNext;

    private MfxPattern mfxPattern;

    public void Init(MfxPattern _mfxPattern)
    {
        mfxPattern = _mfxPattern;
    }

    public void Refresh()
    {
        spellNameTextUpdate.text = mfxPattern.GetElementType().ToString();
        spellNameTextUpdate.color = mfxPattern.GetElementType().GetElementTypeColor();

        string[] difficulties = mfxPattern.GetDifficulty();
        difficultyValueTextActual.text = difficulties[0];
        difficultyValueTextNext.text = difficulties[1];


        Color[] difficultyColors = mfxPattern.GetDifficultyColor();
        difficultyValueTextActual.color = difficultyColors[0];
        difficultyValueTextNext.color = difficultyColors[1];

        //Attack -> def?
        valueTitleTextUpdate.text = mfxPattern.GetSpellTypeUI() + ":";

        string[] values = mfxPattern.GetTypeValueUI();
        valueValueTextActual.text = values[0];
        valueValueTextNext.text = values[1];

        string[] cooldowns = mfxPattern.GetCooldownUI();
        cooldownValueTextActual.text = cooldowns[0];
        cooldownValueTextNext.text = cooldowns[1];

        string[] levels = mfxPattern.GetLevelUI();
        levelTextActual.text = levels[0];
        levelTextNext.text = levels[1];
    }
}
