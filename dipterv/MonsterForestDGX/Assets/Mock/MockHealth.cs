﻿
public class MockHealth : Health
{
    public override void SetDamageBlock(){}

    public override void TakeDamageBasedOnHit(float dmg, bool isHeadshot){}

    protected override float GetBlockedDamage(float dmg)
    {
        return dmg;
    }
}
