
public class MockSpellCaster : SpellCaster
{
    public RecognizingResult result;

    public override void CastBasedOnResult(RecognizingResult _result)
    {
        result = _result;
    }
}
