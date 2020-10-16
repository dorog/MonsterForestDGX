using UnityEngine;

public class MockPetSelector : MonoBehaviour, IPetSelector
{
    public int Id { get; set; }

    public int GetPet()
    {
        return Id;
    }
}
