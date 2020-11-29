using System;
using System.Collections.Generic;

public interface IPetManager
{
    void ChangePetFunction(int id);
    void SubscibeToPetDataLoadedEvent(Action<PetData[]> method);
    void UnsubscibeToPetDataLoadedEvent(Action<PetData[]> method);
    void SubscibeToPetDataChangedEvent(Action<List<PetDataDifference>> method);
    void UnsubscibeToPetDataChangedEvent(Action<List<PetDataDifference>> method);
    void LoadData();
}
