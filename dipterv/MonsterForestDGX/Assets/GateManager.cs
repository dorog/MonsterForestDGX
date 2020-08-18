using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Merge Gate and Alive Monsters Manager
public class GateManager : SingletonClass<GateManager>
{
    public BattlePlace[] battlePlaces;

    private void Awake()
    {
        DataManager dataManager = DataManager.GetInstance();

        bool[] gateStates = dataManager.GetGatesState();

        for (int i = 0; i < gateStates.Length; i++)
        {
            battlePlaces[i].id = i;
            battlePlaces[i].SetAlive(gateStates[i]);
        }

        Init(this);
    }

    public void Won(int id)
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.SaveGateDeath(id);
    }
}
