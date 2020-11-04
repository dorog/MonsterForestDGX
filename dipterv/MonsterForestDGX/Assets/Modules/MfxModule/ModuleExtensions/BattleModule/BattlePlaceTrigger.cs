using UnityEngine;

public class BattlePlaceTrigger : TriggerEvent
{
    public BattlePlace battlePlace;

    public override void TriggerEnter(Collider other)
    {
        //TODO: Rotate body, but headset not rotating

        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;

        battlePlace.Triggered();

        gameObject.SetActive(false);
    }

    public override void TriggerExit(Collider other){}
}
