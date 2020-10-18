using UnityEngine;

public class GateElement : MonoBehaviour, ITarget
{
    public ElementType cristalType;
    public GateHealth gateHealth;

    //TODO: Disappears right know, but add animation instead
    private bool alive = true;

    public void TakeDamage(float dmg)
    {

    }

    public void TakeDamage(float dmg, ElementType elementType)
    {
        if(alive && cristalType == elementType)
        {
            alive = false;
            gateHealth.TakeDamage(1);
            //TODO: Remove if it has animation
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float dmg, Health attackerHealth)
    {
        //TODO: Not need it, Gate can't attack
        if(gateHealth == attackerHealth)
        {
            return;
        }

        //TakeDamage(dmg, cristalType);
    }

    public void ResetElement()
    {
        alive = true;
    }
}
