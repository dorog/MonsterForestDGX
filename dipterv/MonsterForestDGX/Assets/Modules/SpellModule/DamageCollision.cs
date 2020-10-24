using UnityEngine;

public class DamageCollision : MonoBehaviour
{
    public float dmg = 10;
    public ElementType elementType;
    public GameObject impactParticle;

    private void OnCollisionEnter(Collision hit)
    {
        DamageMonster(hit);

        Vector3 impactNormal = hit.contacts[0].normal;

        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

        Destroy(impactParticle, 3f);
        Destroy(gameObject);
    }

    private void DamageMonster(Collision hit)
    {
        ISpellTarget target = hit.gameObject.GetComponent<ISpellTarget>();
        if (target != null)
        {
            target.TakeDamage(dmg, elementType);
        }
    }
}
