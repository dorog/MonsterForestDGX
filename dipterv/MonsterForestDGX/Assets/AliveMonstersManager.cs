﻿
public class AliveMonstersManager : SingletonClass<AliveMonstersManager>
{
    public BattlePlace[] battlePlaces;

    private void Start()
    {
        DataManager dataManager = DataManager.GetInstance();

        bool[] aliveMonsters = dataManager.GetAliveMonsters();

        for (int i = 0; i < aliveMonsters.Length; i++)
        {
            battlePlaces[i].id = i;
            battlePlaces[i].SetAlive(aliveMonsters[i]);
        }

        Init(this);
    }

    public void Won(int id)
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.SaveMonsterDeath(id);
    }
}
