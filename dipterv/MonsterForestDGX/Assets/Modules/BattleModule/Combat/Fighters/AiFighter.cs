
public abstract class AiFighter : Fighter
{
    public override void SetupForFight(Fighter fighter)
    {
        base.SetupForFight(fighter);
        Appear();
    }

    public override void Withdraw()
    {
        Disappear();
    }

    public override void Win()
    {
        ResetMonster();
    }

    public override void Draw(){}

    protected abstract void Appear();
    protected abstract void Disappear();
    protected abstract void ResetMonster();
    public abstract void Disable();
}
