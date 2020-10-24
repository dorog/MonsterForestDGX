using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public float dmg = 10;

    private void OnTriggerEnter(Collider other)
    {
        ITarget target = other.GetComponent<ITarget>();
        if(target != null)
        {
            target.TakeDamage(dmg);

            gameObject.SetActive(false);
        }
    }
}
