using UnityEngine;

public class SpellAttack : PlayerSpell
{
    public float dmg = 10;
    public GameObject impactParticle;
    private bool hasCollided = false;

    private BattleManager battleManager;
    private ExperienceManager experienceManager;

    public void SetBattleManager(BattleManager battleManager, ExperienceManager experienceManager)
    {
        this.battleManager = battleManager;
        this.experienceManager = experienceManager;
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

    private void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            DamageMonster(hit);

            Vector3 impactNormal = hit.contacts[0].normal;

            hasCollided = true;
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

            Destroy(impactParticle, 3f);
            Destroy(gameObject);
        }
    }

    private void VanishSpell()
    {
        hasCollided = true;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        battleManager.RedFighterTurnStartDelegateEvent -= VanishSpell;
        battleManager.Draw -= VanishSpell;
        battleManager.RedFighterWon -= VanishSpell;
        battleManager.BlueFighterWon -= VanishSpell;
    }

    private void DamageMonster(Collision hit)
    {
        ITarget target = hit.gameObject.GetComponent<ITarget>();
        if (target != null)
        {
            experienceManager.AddExp(ExpType.Hit.GetExp() * coverage);
            target.TakeDamage(dmg * coverage);
        }
    }
}
