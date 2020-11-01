using UnityEngine;

//TODO: Rename it
public class SpellAttack : PatternSpell
{
    private BattleManager battleManager;

    public float dmg = 10;
    public ElementType elementType;
    public GameObject impactParticle;

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
        return dmg;
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

    private void OnCollisionEnter(Collision hit)
    {
        DamageTarget(hit);

        Vector3 impactNormal = hit.contacts[0].normal;

        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

        Destroy(impactParticle, 3f);
        Destroy(gameObject);
    }

    private void DamageTarget(Collision hit)
    {
        ISpellTarget target = hit.gameObject.GetComponent<ISpellTarget>();
        if (target != null)
        {
            target.TakeDamage(dmg, elementType);
        }
    }
}
