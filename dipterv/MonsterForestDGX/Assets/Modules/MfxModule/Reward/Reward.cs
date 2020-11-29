using UnityEngine;

public class Reward : MonoBehaviour
{
    public int id;
    public Transform rewardSpot;
    public Fighter fighter;
    public ChestReward reward;

    public DataManager dataManager;

    public RewardEffect[] rewardEffects;

    public void InitReward(int _id, RewardState rewardState)
    {
        id = _id;

        if(rewardState == RewardState.No)
        {
            fighter.SubscribeToDie(FighterDied);
        }
        else if(rewardState == RewardState.Earned)
        {
            ShowReward();
        }
        else
        {
            foreach(var effect in rewardEffects)
            {
                effect.Activate();
            }
        }
    }

    private void FighterDied()
    {
        fighter.UnsubscribeToDie(FighterDied);
        ShowReward();

        dataManager.RewardStateChanged(id, RewardState.Earned);
    }

    private void ShowReward()
    {
        reward.id = id;
        reward.gameObject.SetActive(true);
    }
}
