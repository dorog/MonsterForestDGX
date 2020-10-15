using System.Collections.Generic;

public class PetUnlockerComponent : PetComponent
{
    public PetUnlockSpot[] unlockSpots;

    public override void AddPetManager(IPetManager _petManager)
    {
        base.AddPetManager(_petManager);
        _petManager.SubscibeToPetDataChangedEvent(Refresh);
    }

    private void Refresh(List<PetDataDifference> petDataDifferences)
    {
        for (int i = 0; i < petDataDifferences.Count; i++)
        {
            unlockSpots[petDataDifferences[i].Id].Refresh(petDataDifferences[i].NewAvailability);
        }
    }

    protected override void SetPetDatas(PetData[] _petDatas)
    {
        for (int i = 0; i < _petDatas.Length; i++)
        {
            unlockSpots[i].Setup(_petDatas[i], i, this);
        }
        base.SetPetDatas(_petDatas);
    }

    public void CollectPet(int id)
    {
        petManager.UnlockPet(id);
    }
}
