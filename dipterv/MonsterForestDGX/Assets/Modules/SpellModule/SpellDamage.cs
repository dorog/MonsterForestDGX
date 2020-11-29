using UnityEngine;

public class SpellDamage : MonoBehaviour
{
    public GameObject impactParticle;
    public float dmg = 10;
    public ElementType elementType;

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
