
public interface IEnemy
{
    void Appear();
    void Disappear();
    void ResetMonster();
    Health GetHealth();
    bool IsMonster();
    void Fight();
    void Disable();
}
