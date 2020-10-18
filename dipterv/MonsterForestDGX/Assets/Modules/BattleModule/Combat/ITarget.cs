
public interface ITarget
{
    void TakeDamage(float dmg, ElementType elementType);
    void TakeDamage(float dmg, ElementType elementType, Health health);
}
