using UnityEngine;

public class MockSpellCastingHandler : SpellCastingHandler
{
    public RecognizingResult result;
    public Vector2 guess;
    public bool setted = false;

    public override RecognizingResult GetResult()
    {
        return result;
    }

    public override void Guess(Vector2 _guess)
    {
        if (!setted)
        {
            setted = true;
            guess = _guess;
        }
    }

    public override void ResetHandler()
    {
        result = null;
        setted = false;
    }
}
