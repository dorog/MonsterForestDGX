
public class MagicCircleSpellCaster : SpellCaster
{
    public MagicCircleHandler magicCircleHandler;

    public override void CastBasedOnResult(RecognizingResult result)
    {
        if (result == null)
        {
            magicCircleHandler.CastFailed();
        }
        else
        {
            magicCircleHandler.CastSpell(result);
        }
    }
}
