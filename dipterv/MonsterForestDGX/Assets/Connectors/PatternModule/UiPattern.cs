using System.Collections.Generic;
using UnityEngine;

public class UiPattern : MonoBehaviour, IShopUiPattern
{
    public Sprite icon;
    public int level = 0;

    public PlayerSpell[] Spells;
    public int[] RequiredExps;
    public ElementType ElementType;

    public UpdateablePetUI patternUI;
    private UpdateablePetUI ui;

    private PatternFormula patternFormula;

    public SpellElementInfoUI spellElementInfoUI;

    private int id;
    private PatternShopComponent patternShopComponent;
    private PatternInfoComponent patternInfoComponent;

    public void Init(int _id, List<Vector2> points, Sprite _icon, PatternShopComponent _patternShopComponent, PatternInfoComponent _patternInfoComponent, float width = 10)
    {
        id = _id;
        icon = _icon;
        patternShopComponent = _patternShopComponent;
        patternInfoComponent = _patternInfoComponent;
        patternFormula = new PatternFormula(points, width);
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
        if (IsMaxed())
        {
            return new string[] { "Max" };
        }
        else if (level == 0)
        {
            return new string[] { level.ToString() };
        }
        else
        {
            return new string[] { level.ToString(), (level + 1).ToString() };
        }
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
        return RequiredExps[level];
    }

    public bool IsMaxed()
    {
        return level == Spells.Length;
    }

    public void LevelUp()
    {
        level++;
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
        int price = GetRequiredExpValue();
        level++;
        patternShopComponent.ChangeQuantity(id, price);
    }

    public void ShowInfo()
    {
        patternInfoComponent.SelectPattern(id);
    }
}
