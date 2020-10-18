using UnityEngine;

public class Attack : MonoBehaviour
{
    public float dmg = 10;
    public ElementType magicType = ElementType.TrueDamage;
    public Health ownHealth;

    private bool Used = false;

    private void OnEnable()
    {
        Used = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Used)
        {
            return;
        }

        ITarget target = other.GetComponent<ITarget>();
        if(target == null)
        {
            return;
        }

        Used = true;
        target.TakeDamage(dmg, magicType, ownHealth);

        gameObject.SetActive(false);
    }
}
