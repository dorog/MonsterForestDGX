
//TODO: Rename it
public class SpellAttack : PatternSpell
{
    private RoundHandler roundHandler;
    private BattleManager battleManager;

    public SpellDamage spellDamage;

    public void SetBattleManager(RoundHandler roundHandler, BattleManager battleManager)
    {
        this.roundHandler = roundHandler;
        this.battleManager = battleManager;

        roundHandler.SubscribeToEndTurn(VanishSpell);
        battleManager.Draw += VanishSpell;
        battleManager.RedFighterWon += VanishSpell;
        battleManager.BlueFighterWon += VanishSpell;
    }

    public override string GetSpellType()
    {
        return "Damage";
    }

    public override float GetSpellTypeValue()
    {
        return spellDamage.dmg;
    }

    private void VanishSpell()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        roundHandler.UnsubscribeToEndTurn(VanishSpell);
        battleManager.Draw -= VanishSpell;
        battleManager.RedFighterWon -= VanishSpell;
        battleManager.BlueFighterWon -= VanishSpell;
    }
}
