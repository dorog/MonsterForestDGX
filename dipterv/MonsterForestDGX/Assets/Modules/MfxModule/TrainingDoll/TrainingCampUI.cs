using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class TrainingCampUI : MonoBehaviour
{
    public Slider cooldownSlider;

    public CooldownResetPetAbility cooldownReset;

    public GameObject root;

    public Text percent;

    public Dropdown dropdown;

    public SpellGuideDrawer spellGuideDrawer;


    public Toggle coverageToogle;
    public TraningCampCoverageUI coverageUI;

    public Toggle damageToogle;
    public GameObject damageUI;

    public MfxTraningCampPatternComponent mfxTraningCampPatternComponent;

    private void Start()
    {
        SetCooldownChance();
    }

    public void EnableUI()
    {
        root.SetActive(true);
    }

    public void DisableUI()
    {
        root.SetActive(false);
        spellGuideDrawer.ClearGuide();
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
        OptionData option = dropdown.options[dropdown.value];
        PatternSpellData basePatternSpell = mfxTraningCampPatternComponent.GetSpellPoints(option.text);

        ElementType elementType = ElementTypeExtensions.GetElementTypeByName(option.text);

        coverageUI.FilterSpellCoverage(elementType);

        if (basePatternSpell == null)
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
        coverageUI.gameObject.SetActive(coverageToogle.isOn);
    }

    public void SetDamageVisibility()
    {
        damageUI.SetActive(damageToogle.isOn);
    }
}
