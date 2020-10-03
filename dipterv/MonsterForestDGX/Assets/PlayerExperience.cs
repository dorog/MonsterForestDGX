using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : SingletonClass<PlayerExperience>
{
    private float exp = 0;
    public SpellsUI spellsUI;
    //public Text expText;

    public event Action<float> ExpChanged;

    private void Awake()
    {
        Init(this);
        DataManager dataManager = DataManager.GetInstance();
        dataManager.expChangedEvent += SetExp;
    }

    public float GetExp()
    {
        return exp;
    }

    public void SetExp(float exp)
    {
        this.exp = exp;
        int expValue = Mathf.FloorToInt(exp);

        ExpChanged?.Invoke(expValue);
    }

    public void AddExp(ExpType expType, float coverage)
    {
        SetExp(exp + expType.GetExp() * coverage);
    }

    public void AddExp(float addedExp, float coverage)
    {
        SetExp(exp + addedExp * coverage);
    }
}
