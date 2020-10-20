
//TODO: Rename it
public class SpellAttack : PatternSpell
{
    private BattleManager battleManager;

    public void SetBattleManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        battleManager.RedFighterTurnStartDelegateEvent += VanishSpell;
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
        return 0;
        //return dmg;
    }

    private void VanishSpell()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        battleManager.RedFighterTurnStartDelegateEvent -= VanishSpell;
        battleManager.Draw -= VanishSpell;
        battleManager.RedFighterWon -= VanishSpell;
        battleManager.BlueFighterWon -= VanishSpell;
    }
}
