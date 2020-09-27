using UnityEngine;
using UnityEngine.UI;

public class PetAbilityDesciptionElementUI : MonoBehaviour
{
    public Text nameText;
    public Text decriptionText;

    public void Init(PetAbilityDesciption petAbilityDesciption)
    {
        nameText.text = petAbilityDesciption.Name;
        nameText.color = petAbilityDesciption.Color;
        decriptionText.text = petAbilityDesciption.Description;
    }
}
