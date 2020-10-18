using UnityEngine;
using UnityEngine.UI;

public class TextHealthShowerUI : HealthShowerUI
{
    public Text hp;

    public override void ShowHealthData(float currentHp, float maxHp)
    {
        if(currentHp <= 0)
        {
            hp.text = "Dead";
        }
        else
        {
            hp.text = Mathf.Ceil(currentHp).ToString() + "/" + maxHp.ToString();
        }
    }
}
