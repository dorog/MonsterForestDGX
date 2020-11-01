using UnityEngine;

public class PatternSpellCastingHandler : SpellCastingHandler
{
    public PatternRecognizerComponent patternRecognizer;
    public float scale = 200;

    public override RecognizingResult GetResult()
    {
        return patternRecognizer.GetResult();
    }

    public override void Guess(Vector2 guess)
    {
        patternRecognizer.Guess(guess * scale);
    }

    public override void ResetHandler()
    {
        patternRecognizer.ResetSpells();
    }

    private void OnDisable()
    {
        ResetHandler();
    }
}
