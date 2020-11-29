using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyGroup[] battlePlaces;

    public GameEvents gameEvents;
    public DataManager dataManager;

    public void Start()
    {
        List<EnemyGroupData> enemys = dataManager.GetEnemies();

        SetBattlePlacesState(enemys, battlePlaces);

        gameEvents.BattleStartDelegateEvent += SubscribeToMonsterDeath;
        gameEvents.BattleEndDelegateEvent += UnsubscribeToMonsterDeath;
    }

    private void SetBattlePlacesState(List<EnemyGroupData> states, EnemyGroup[] battlePlaces)
    {
        for(int i = 0; i < states.Count; i++)
        {
            var enemyGroup = states.ElementAt(i);
            for (int j = 0; j < enemyGroup.enemyStates.Length; j++)
            {
                battlePlaces[i].battlePlaces[j].id = j;
                battlePlaces[i].battlePlaces[j].group = enemyGroup.group;
                battlePlaces[i].battlePlaces[j].SetAlive(!enemyGroup.enemyStates[j]);
            }
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
        dataManager.SaveEnemyDeath(gameEvents.group, gameEvents.id);
    }
}
