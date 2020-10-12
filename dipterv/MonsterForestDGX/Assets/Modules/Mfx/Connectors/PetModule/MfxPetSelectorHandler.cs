using UnityEngine;

public class MfxPetSelectorHandler : MonoBehaviour, IPetSelectorHandler
{
    public DataManager dataManager;

    public int GetLastSelectedPet()
    {
        return dataManager.GetLastSelectedPet();
    }

    public void Select(int id)
    {
        dataManager.SaveSelectedPet(id);
    }
}
