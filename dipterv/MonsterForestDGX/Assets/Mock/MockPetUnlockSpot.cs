
public class MockPetUnlockSpot : PetUnlockSpot
{
    public PetData PetData { get; set; }
    public int Id { get; set; }
    private PetUnlockerComponent petUnlockerComponent;

    public override void Refresh(bool available)
    {
        PetData.available = available;
    }

    public override void Setup(PetData _petData, int _id, PetUnlockerComponent _petUnlockerComponent)
    {
        PetData = _petData;
        Id = _id;
        petUnlockerComponent = _petUnlockerComponent;
    }

    public void UnlockPet()
    {
        petUnlockerComponent.CollectPet(Id);
    }
}
