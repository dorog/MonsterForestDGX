
public class MovingFighterBody : ExtraFighterPart
{
    public TurnFill positionTurnFill;
    public TurnFill disableTurnFill;

    public MovingAxis axis;
    public bool revert = false;

    public override void Appear()
    {
        StartCoroutine(positionTurnFill.Move(axis.GetPositiveDirection(revert)));
    }

    public override void Disappear()
    {
        StartCoroutine(positionTurnFill.Move(axis.GetNegtivDirection(revert)));
    }

    public override void Disable()
    {
        StartCoroutine(disableTurnFill.Move(axis.GetPositiveDirection(revert)));
    }
}
