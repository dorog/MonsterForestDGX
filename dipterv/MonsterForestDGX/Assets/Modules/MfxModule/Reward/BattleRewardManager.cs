using UnityEngine;

public class BattleRewardManager : MonoBehaviour
{
    public DataManager dataManager;
    public Reward[] rewards;

    void Start()
    {
        RewardState[] states = dataManager.GetRewardStates();
        for(int i = 0; i < states.Length; i++)
        {
            rewards[i].InitReward(i, states[i]);
        }
    }
}
