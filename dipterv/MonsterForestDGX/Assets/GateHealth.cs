using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHealth : Health
{
    public GateElement[] gateElements;

    private void Awake()
    {
        maxHp = gateElements.Length;
    }

    public override void SetDamageBlock()
    {
        
    }

    protected override float GetBlockedDamage(float dmg)
    {
        return dmg;
    }

    public override void ResetHealth()
    {
        base.ResetHealth();
        foreach(var gateElement in gateElements)
        {
            //TODO: Remove if gateElements has animation
            gateElement.gameObject.SetActive(true);
            gateElement.ResetElement();
        }
    }

    public override void TakeDamageBasedOnHit(float dmg, ElementType magicType, bool isHeadshot)
    {
        
    }
}
