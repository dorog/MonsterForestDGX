using System.Collections.Generic;

public class PetUnlockerComponent : PetComponent
{
    public PetUnlockSpot[] unlockSpots;

    public override void AddPetManager(IPetManager _petManager)
    {
        base.AddPetManager(_petManager);
        petManager.SubscibeToPetDataChangedEvent(Refresh);
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
        base.SetPetDatas(_petDatas);
        for (int i = 0; i < petDatas.Length; i++)
        {
            unlockSpots[i].Setup(petDatas[i], i, this);
        }
    }

    public void CollectPet(int id)
    {
        petManager.ChangePetFunction(id);
    }
}
