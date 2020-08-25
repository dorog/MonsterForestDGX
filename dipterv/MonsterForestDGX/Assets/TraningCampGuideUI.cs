using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraningCampGuideUI : MonoBehaviour
{
    private SpellManager spellManager = null;

    public Dropdown dropdown;

    private void OnEnable()
    {
        if(spellManager == null)
        {
            spellManager = SpellManager.GetInstance();
        }

        List<string> spellNames = spellManager.GetAttackSpellNames();

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
