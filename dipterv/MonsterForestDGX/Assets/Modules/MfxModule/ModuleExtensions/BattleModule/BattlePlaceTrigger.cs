using UnityEngine;

public class BattlePlaceTrigger : MonoBehaviour
{
    public BattlePlace battlePlace;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //TODO: Rotate body, but headset not rotating

            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            battlePlace.Triggered();

            gameObject.SetActive(false);
        }
    }
}
