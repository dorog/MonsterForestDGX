using UnityEngine;

public class ContinousDamageTrigger : MonoBehaviour
{
    public float dmg = 2;
    private float time = 0;

    private void OnTriggerEnter(Collider other)
    {
        ITarget target = other.GetComponent<ITarget>();
        if (target != null)
        {
            target.TakeDamage(dmg * time);
            time = 0;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
    }
}
