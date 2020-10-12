using UnityEngine;

public class MockSpellHandler : MonoBehaviour, ISpellHandler
{
    public MockDataIO mockDataIO;
    public MagicCircleHandler magicCircleHandler;
    public TextCooldownShower cooldownShower;

    public void Start()
    {
        magicCircleHandler.spellHandler = this;
        magicCircleHandler.cooldownShower = cooldownShower;
    }

    public SpellData GetSpellData(int index)
    {
        GameData gameData = mockDataIO.Read();

        SpellData spellData = new SpellData()
        {
            //TODO: Refactor this!
            Cooldown = mockDataIO.gameConfig.baseSpells[index].basePatternSpell.levelsSpell[gameData.basePatternSpellLevels[index]].playerSpell.cd,
            ElementType = mockDataIO.gameConfig.baseSpells[index].basePatternSpell.elementType,
            Spell = mockDataIO.gameConfig.baseSpells[index].basePatternSpell.levelsSpell[gameData.basePatternSpellLevels[index]].playerSpell.gameObject
        };

        return spellData;
    }
}
