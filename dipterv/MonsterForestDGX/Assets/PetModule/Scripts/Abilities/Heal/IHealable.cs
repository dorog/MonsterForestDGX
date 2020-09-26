
public interface IHealable : IPetParameter
{
    bool IsFull();
    void Heal(float amount);
}
