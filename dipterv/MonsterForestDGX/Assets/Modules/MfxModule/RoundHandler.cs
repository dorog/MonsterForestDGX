using System;
using UnityEngine;

public class RoundHandler : MonoBehaviour
{
    private event Action StartTurn;
    private event Action EndTurn;

    public void SubscribeToStartTurn(Action method)
    {
        StartTurn += method;
    }
    public void UnsubscribeToStartTurn(Action method)
    {
        StartTurn -= method;
    }
    public void SubscribeToEndTurn(Action method)
    {
        EndTurn += method;
    }
    public void UnsubscribeToEndTurn(Action method)
    {
        EndTurn -= method;
    }

    public void Fight()
    {
        StartTurn?.Invoke();
    }

    public void Def()
    {
        EndTurn?.Invoke();
    }
}
