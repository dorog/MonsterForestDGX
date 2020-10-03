using UnityEngine;

public interface IUiPattern : IPattern
{
    Sprite GetIcon();
    string GetSpellTypeUI();
    string[] GetLevelUI();
    string[] GetTypeValueUI();
    string[] GetDifficulty();
    Color[] GetDifficultyColor();
    string GetRequiredExp();
    int GetRequiredExpValue();
    string[] GetCooldownUI();
    bool IsMaxed();
    void LevelUp();
    void InstantiateUiElement(Transform root, int quantity);
}
