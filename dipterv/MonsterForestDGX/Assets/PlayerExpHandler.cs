using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpHandler : MonoBehaviour
{
    private PlayerExperience playerExperience;
    private float lastCoverage = 0;

    void Start()
    {
        Debug.Log("Connect this class functions!");
    }

    //TODO: Save last hit coverage and add the xp
    public void AddXpForHit(float coverage)
    {
        lastCoverage = coverage;
        playerExperience.AddExp(ExpType.Hit, coverage);
    }

    //TODO: 
    public void Won()
    {
        playerExperience.AddExp(ExpType.Kill, lastCoverage);

        DataManager.GetInstance().Won(playerExperience.GetExp());
    }
}
