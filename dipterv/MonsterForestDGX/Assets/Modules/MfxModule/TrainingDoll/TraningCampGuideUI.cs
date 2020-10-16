using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraningCampGuideUI : MonoBehaviour
{
    public Dropdown dropdown;

    public MfxTraningCampPatternComponent patternTraningCampPatternComponent;

    private void OnEnable()
    {
        List<string> spellNames = patternTraningCampPatternComponent.GetAttackSpellNames();

        if(spellNames.Count == 0)
        {
            dropdown.gameObject.SetActive(false);
        }
        else
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>
            {
                new Dropdown.OptionData("No")
            };
            foreach (var spellName in spellNames)
            {
                options.Add(new Dropdown.OptionData(spellName));
            }

            dropdown.options = options;
            dropdown.gameObject.SetActive(true);
        }
    }
}
