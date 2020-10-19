using UnityEngine;

public class DollHealth : Health
{
    [Header ("Doll settings")]
    public TraningCampDamageUI damageUI;

    protected override float GetBlockedDamage(float dmg)
    {
        damageUI.ShowDamage(dmg);

        return 0;
    }
}
