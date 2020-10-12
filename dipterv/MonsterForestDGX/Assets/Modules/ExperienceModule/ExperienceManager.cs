using System;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    private float exp = 0;
    private float stack = 0;

    private event Action<float> ExpChanged;

    public IExperienceIO ExperienceIO { get; set; }

    public void Load()
    {
        exp = ExperienceIO.LoadExp();
        ExpChanged?.Invoke(exp);
    }

    public void AddExp(float addedExp)
    {
        stack += addedExp;
    }

    public void RemoveExp(float removedExp)
    {
        stack -= removedExp;
    }

    public void ResetExp()
    {
        stack = 0;
    }

    public void Save()
    {
        exp += stack;
        stack = 0;
        ExperienceIO.SaveExp(exp);

        ExpChanged?.Invoke(exp);
    }

    public void SubscribeToExpChanged(Action<float> method)
    {
        ExpChanged += method;
    }

    public void UnsubscribeToExpChanged(Action<float> method)
    {
        ExpChanged += method;
    }
}
