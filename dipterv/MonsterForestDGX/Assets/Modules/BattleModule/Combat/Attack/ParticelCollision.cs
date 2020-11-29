using UnityEngine;

public class ParticelCollision : MonoBehaviour
{
    public float Damage = 10;

    private void OnParticleCollision(GameObject other)
    {
        ITarget target = other.GetComponent<ITarget>();
        if(target != null)
        {
            target.TakeDamage(Damage);
        }
    }
}
