using UnityEngine;

public class GateElement : MonoBehaviour, ITarget
{
    public ElementType elementType;
    public GateHealth gateHealth;

    //TODO: Disappears right know, but add animation instead
    private bool alive = true;

    public void TakeDamage(float dmg, ElementType spellType)
    {
        if(alive && elementType == spellType)
        {
            alive = false;
            gateHealth.TakeDamage(1, spellType);
            //TODO: Remove if it has animation
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float dmg, ElementType elementType, Health attackerHealth)
    {
        //TODO: Not need it, Gate can't attack
        if(gateHealth == attackerHealth)
        {
            return;
        }

        TakeDamage(dmg, elementType);
    }

    public void ResetElement()
    {
        alive = true;
    }
}
