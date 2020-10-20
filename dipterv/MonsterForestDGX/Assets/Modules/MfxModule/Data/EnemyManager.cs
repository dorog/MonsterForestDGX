
public class EnemyManager
{
    public BattlePlace[] monsterBattlePlaces;
    public BattlePlace[] gateBattlePlaces;

    public GameEvents gameEvents;
    public DataManager dataManager;

    public void Start()
    {
        bool[] aliveMonsters = dataManager.GetAliveMonsters();
        bool[] gateStates = dataManager.GetGatesState();

        SetBattlePlacesState(aliveMonsters, monsterBattlePlaces);
        SetBattlePlacesState(gateStates, gateBattlePlaces);

        gameEvents.BattleStartDelegateEvent += SubscribeToMonsterDeath;
        gameEvents.BattleEndDelegateEvent += UnsubscribeToMonsterDeath;
    }

    private void SetBattlePlacesState(bool[] states, BattlePlace[] battlePlaces)
    {
        for (int i = 0; i < states.Length; i++)
        {
            battlePlaces[i].id = i;
            battlePlaces[i].SetAlive(states[i]);
        }
    }

    private void SubscribeToMonsterDeath()
    {
        gameEvents.enemy.SubscribeToDie(Won);
    }

    private void UnsubscribeToMonsterDeath()
    {
        gameEvents.enemy.UnsubscribeToDie(Won);
    }

    public void Won()
    {
        dataManager.SaveMonsterDeath(gameEvents.id);
    }
}
