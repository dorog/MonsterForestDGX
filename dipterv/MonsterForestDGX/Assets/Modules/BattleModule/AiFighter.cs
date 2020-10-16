﻿
public abstract class AiFighter : Fighter
{
    public override void SetupForFight()
    {
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

    public override void Draw()
    {
        
    }

    protected abstract void Appear();
    protected abstract void Disappear();
    protected abstract void ResetMonster();
    public abstract EnemyType IsMonster();
    public abstract void Disable();
}
