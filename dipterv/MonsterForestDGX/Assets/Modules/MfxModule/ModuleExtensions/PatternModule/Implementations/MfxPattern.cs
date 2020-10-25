using System.Collections.Generic;
using UnityEngine;

public class MfxPattern : MonoBehaviour, IShopUiPattern, IUiPattern
{
    private PatternFormula patternFormula;

    private int id;
    private ExperienceManager experienceManager;
    private MfxPatternManager patternManager;

    [Header ("Settings")]
    public Sprite icon;
    public int level = 0;

    public PatternSpell[] Spells;
    public int[] RequiredExps;
    public ElementType ElementType;

    [Header ("ShopUI")]
    public UpdateablePatternUI patternUI;
    private UpdateablePatternUI ui;

    [Header ("InfoUI")]
    public MfxPatternInfoUI spellElementInfoUI;
    private MfxPatternInfoUI spellElementInfo;

    public void Init(int _id, List<Vector2> points, Sprite _icon, float width = 10)
    {
        id = _id;
        icon = _icon;

        patternFormula = new PatternFormula(points, width);
    }

    public void Connect(MfxPatternManager _patternManager, ExperienceManager _experienceManager)
    {
        patternManager = _patternManager;
        experienceManager = _experienceManager;
    }

    public GameObject GetSpell()
    {
        return Spells[level - 1].gameObject;
    }

    public ElementType GetElementType()
    {
        return ElementType;
    }

    public int GetLevelValue()
    {
        return level;
    }

    public float GetMinCoverage()
    {
        if (level == 0)
        {
            return 2;
        }
        return Spells[level - 1].coverage;
    }

    //UI:
    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetSpellTypeUI()
    {
        if (level != 0)
        {
            return Spells[level - 1].GetSpellType().ToString();
        }
        else
        {
            return Spells[level].GetSpellType().ToString();
        }
    }

    public string[] GetLevelUI()
    {
        //TODO: Add max instead of lvl + 1?
        return new string[] { level.ToString(), (level + 1).ToString() };
    }

    public string[] GetTypeValueUI()
    {
        if (IsMaxed())
        {
            return new string[] { Spells[level - 1].GetSpellTypeValue().ToString() };
        }
        else if (level == 0)
        {
            return new string[] { Spells[level].GetSpellTypeValue().ToString() };
        }
        else
        {
            return new string[] { Spells[level - 1].GetSpellTypeValue().ToString(), Spells[level].GetSpellTypeValue().ToString() };
        }
    }

    public float GetCooldown()
    {
        return Spells[level - 1].cd;
    }

    public string[] GetDifficulty()
    {
        if (IsMaxed())
        {
            return new string[] { Spells[level - 1].GetDifficulty() };
        }
        else if (level == 0)
        {
            return new string[] { Spells[level].GetDifficulty() };
        }
        else
        {
            return new string[] { Spells[level - 1].GetDifficulty(), Spells[level].GetDifficulty() };
        }
    }

    public Color[] GetDifficultyColor()
    {
        if (IsMaxed())
        {
            return new Color[] { Spells[level - 1].GetDifficultyColor() };
        }
        else if (level == 0)
        {
            return new Color[] { Spells[level].GetDifficultyColor() };
        }
        else
        {
            return new Color[] { Spells[level - 1].GetDifficultyColor(), Spells[level].GetDifficultyColor() };
        }
    }

    public string GetRequiredExp()
    {
        if (IsMaxed())
        {
            return "---";
        }
        else
        {
            return RequiredExps[level].ToString();
        }
    }

    public string[] GetCooldownUI()
    {
        if (IsMaxed())
        {
            return new string[] { Spells[level - 1].cd.ToString() };
        }
        else if (level == 0)
        {
            return new string[] { Spells[level].cd.ToString() };
        }
        else
        {
            return new string[] { Spells[level - 1].cd.ToString(), Spells[level].cd.ToString() };
        }
    }

    public int GetRequiredExpValue()
    {
        if (IsMaxed())
        {
            return int.MaxValue;
        }
        else if(level < 0)
        {
            return 0;
        }
        return RequiredExps[level];
    }

    public bool IsMaxed()
    {
        return level == Spells.Length;
    }

    public PatternState GetState()
    {
        if (level < 0)
        {
            return PatternState.Unavailable;
        }
        else if (level == 0)
        {
            return PatternState.Showable;
        }
        else
        {
            return PatternState.Available;
        }
    }

    //TODO: Test Instantiating with unavailable UI
    public void InstantiateUiElement(Transform root, int quantity)
    {
        ui = Instantiate(patternUI, root);
        ui.Init(this, quantity);
        
        if(GetState() == PatternState.Unavailable)
        {
            ui.gameObject.SetActive(false);
        }
    }

    public void Guess(Vector2 point)
    {
        patternFormula.Guess(point);
    }

    public float GetResult()
    {
        return patternFormula.GetResult();
    }

    public void ResetPattern()
    {
        patternFormula.Reset();
    }

    public void RefreshQuantity(int quantity)
    {
        ui.RefreshQuantity(quantity);
    }

    public void RefreshData()
    {
        ui.RefreshData();
    }

    public void Increase()
    {
        experienceManager.RemoveExp(GetRequiredExpValue());

        PatternState oldState = GetState();
        level++;
        PatternState newState = GetState();

        experienceManager.Save();
        if(oldState != newState)
        {
            patternManager.ChangePatternDataState(id, newState);
        }
        else
        {
            patternManager.ChangedPatternData(id);
        }
    }

    public void ShowInfo()
    {
        patternManager.SelectPatternData(id);
    }

    public string GetName()
    {
        return ElementType.ToString();
    }

    public void InstantiateInfo(Transform root)
    {
        spellElementInfo = Instantiate(spellElementInfoUI, root);
        spellElementInfo.SetPattern(this);
    }

    public void RefreshInfo()
    {
        spellElementInfo.Refresh();
    }

    public void ChangeVisibility()
    {
        spellElementInfo.gameObject.SetActive(!spellElementInfo.gameObject.activeSelf);
    }
}
