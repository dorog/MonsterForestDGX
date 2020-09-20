using System;

public class BattleEvents : SingletonClass<BattleEvents>
{
    private event Action<BattleManager> BattleStartDelegateEvent;
    private event Action<BattleManager> BattleEndDelegateEvent;

    public void Awake()
    {
        Init(this);
    }

    public void SubscribeEvents(Action<BattleManager> start, Action<BattleManager> end)
    {
        BattleStartDelegateEvent += start;
        BattleEndDelegateEvent += end;
    }

    public void Fight(BattleManager battleManager)
    {
        BattleStartDelegateEvent?.Invoke(battleManager);
    }

    public void Explore(BattleManager battleManager)
    {
        BattleEndDelegateEvent?.Invoke(battleManager);
    }
}
