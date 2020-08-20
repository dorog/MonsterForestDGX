using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class TrainingCampUI : MonoBehaviour
{

    public Slider cooldownSlider;

    public CooldownResetPetAbility cooldownReset;

    public GameObject root;

    public Text percent;

    private SpellManager spellManager;

    public Dropdown dropdown;

    public SpellGuideDrawer spellGuideDrawer;


    public Toggle coverageToogle;
    public GameObject coverageUI;

    public Toggle damageToogle;
    public GameObject damageUI;

    private void Start()
    {
        spellManager = SpellManager.GetInstance();
        SetCooldownChance();
    }

    public void EnableUI()
    {
        root.SetActive(true);
    }

    public void DisableUI()
    {
        root.SetActive(false);
    }

    public void StepSlider(float value)
    {
        cooldownSlider.value += value;
    }

    public void SetCooldownChance()
    {
        cooldownReset.resetChance = cooldownSlider.value * 100;
        percent.text = (cooldownSlider.value * 100) + "%";
    }

    public void AskForGuide()
    {
        //dropdown.value
        OptionData option = dropdown.options[dropdown.value];
        BasePatternSpell basePatternSpell = spellManager.GetSpellPoints(option.text);

        if(basePatternSpell == null)
        {
            spellGuideDrawer.ClearGuide();
        }
        else
        {
            spellGuideDrawer.DrawGuide(basePatternSpell.SpellPatternPoints.GetPoints());
        }
    }

    public void SetCoverageVisibility()
    {
        coverageUI.SetActive(coverageToogle.isOn);
    }

    public void SetDamageVisibility()
    {
        damageUI.SetActive(damageToogle.isOn);
    }
}
