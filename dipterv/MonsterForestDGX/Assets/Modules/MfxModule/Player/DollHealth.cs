using UnityEngine;

public class DollHealth : Health
{
    [Header ("Doll settings")]
    public TraningCampDamageUI damageUI;

    public override void TakeDamage(float dmg)
    {
        damageUI.ShowDamage(dmg);
    }
}
