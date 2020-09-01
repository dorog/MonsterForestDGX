using UnityEngine;

public class BattlePlaceTrigger : MonoBehaviour
{
    public BattlePlace battlePlace;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;

            battlePlace.Triggered();

            gameObject.SetActive(false);
        }
    }
}
