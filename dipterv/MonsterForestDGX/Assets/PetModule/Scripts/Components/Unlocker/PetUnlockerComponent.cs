using System.Collections.Generic;
using UnityEngine;

public class PetUnlockerComponent : PetComponent
{
    private static readonly int defaultPetId = -1;
    private int availablePetId = defaultPetId;

    public PetEnable[] petEnables;

    public override void Init(IPetManager _petManager)
    {
        base.Init(_petManager);
        _petManager.SubscibeToPetDataChangedEvent(Refresh);
    }

    private void Refresh(List<PetDataDifference> petDataDifferences)
    {
        for (int i = 0; i < petDataDifferences.Count; i++)
        {
            petEnables[petDataDifferences[i].Id].Refresh(petDataDifferences[i].NewAvailability);
        }
    }

    protected override void SetPetDatas(PetData[] _petDatas)
    {
        for (int i = 0; i < _petDatas.Length; i++)
        {
            petEnables[i].Setup(_petDatas[i], i, this);
        }
        base.SetPetDatas(_petDatas);
    }

    public void SetAvailablePet(int id)
    {
        Debug.Log("Set to conncetion class");
        //collectInput.Activate();
        availablePetId = id;
    }

    public void DisableAvailablePet()
    {
        //collectInput.Deactivate();
        availablePetId = defaultPetId;
    }

    /*private void Collect()
    {
        if (availablePetId != defaultPetId)
        {
            CollectPet();
            DisableAvailablePet();
        }
    }
    */

    public void CollectPet()
    {
        if (availablePetId != defaultPetId)
        {
            petManager.UnlockPet(availablePetId);
        }
    }
}
