using UnityEngine;

public abstract class PetComponent : MonoBehaviour
{
    protected IPetManager petManager;
    protected PetData[] petDatas;

    public virtual void Init(IPetManager _petManager)
    {
        petManager = _petManager;
        petManager.SubscibeToPetDataLoadedEvent(SetPetDatas);
    }

    protected virtual void SetPetDatas(PetData[] _petDatas)
    {
        petDatas = _petDatas;
    }
}
