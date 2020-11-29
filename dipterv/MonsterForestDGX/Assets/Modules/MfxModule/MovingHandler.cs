using System;
using UnityEngine;

public class MovingHandler : MonoBehaviour
{
    public event Action Stop;
    public event Action Go;

    public Player player;
    public GameEvents gameEvents;

    void Start()
    {
        gameEvents.BattleEndDelegateEvent += EnableMovement;
        gameEvents.BattleLobbyEnteredDelegateEvent += DisableMovement;
    }

    public void EnableMovement()
    {
        Go?.Invoke();
    }

    public void DisableMovement()
    {
        Stop?.Invoke();
    }
}
