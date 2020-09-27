using UnityEngine;

public class MfxPetSelectorHandler : MonoBehaviour, IPetSelectorHandler
{
    public int GetLastSelectedPet()
    {
        DataManager dataManager = DataManager.GetInstance();
        return dataManager.GetLastSelectedPet();
    }

    public void Select(int id)
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.SaveSelectedPet(id);
    }
}
