using UnityEngine;
using UnityEngine.UI;
public class TraningCampDamageUI : MonoBehaviour
{
    public Text dmgText;

    private void OnEnable()
    {
        dmgText.text = "";
    }

    public void ShowDamage(float dmg)
    {
        dmgText.text = dmg.ToString("0");
    }
}
