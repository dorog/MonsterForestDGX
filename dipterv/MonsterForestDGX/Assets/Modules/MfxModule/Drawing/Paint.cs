using System;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpellCastingHandler spellCastingHandler;

    public SpellCaster spellCaster;

    public IPressed drawingInput;

    public Transform hand;

    public BattleManager battleManager;

    public bool circleOn = false;

    public KeyBindingManager keyBindingManager;
    public GameEvents gameEvents;

    public void Start()
    {
        drawingInput = keyBindingManager.paintingTrigger;

        drawingInput.SubscribeToPressing(Pressing);
        drawingInput.SubscribeToReleased(CheckResult);

        gameEvents.BattleStartDelegateEvent += Fighting;
        gameEvents.BattleEndDelegateEvent += Exploring;
    }

    private void Fighting()
    {
        battleManager.BlueFighterTurnStartDelegateEvent += drawingInput.Activate;
        battleManager.RedFighterTurnStartDelegateEvent += drawingInput.Deactivate;
    }

    private void Exploring()
    {
        battleManager.BlueFighterTurnStartDelegateEvent -= drawingInput.Activate;
        battleManager.RedFighterTurnStartDelegateEvent -= drawingInput.Deactivate;

        drawingInput.Deactivate();
    }

    private void Pressing()
    {
        if (circleOn)
        {
            try
            {
                Vector3 relativPosition = hand.position - spellCaster.transform.position;
                Vector3 projected = Vector3.ProjectOnPlane(relativPosition, spellCaster.transform.forward);
                Vector3 flattenedVector = spellCaster.transform.position + projected;

                Vector2 guess = GetGuess(flattenedVector, spellCaster.transform);

                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, flattenedVector);
                spellCastingHandler.Guess(guess);

            }
            catch (Exception) { }
        }
    }

    private Vector2 GetGuess(Vector3 point, Transform plane)
    {
        Vector3 relativ = point - plane.position;

        float angle = Vector3.SignedAngle(relativ, plane.up, plane.forward);
        int signalX = 1;
        if (angle < 0)
        {
            signalX = -1;
        }

        int signalY = 1;
        if (Mathf.Abs(angle) > 90)
        {
            signalY = -1;
        }

        Vector3 up = Vector3.ProjectOnPlane(relativ, plane.up);
        float x = up.magnitude * signalX;

        Vector3 right = Vector3.ProjectOnPlane(relativ, plane.right);
        float y = right.magnitude * signalY;

        return new Vector2(x, y);
    }

    private void CheckResult()
    {
        if (lineRenderer.positionCount != 0)
        {
            lineRenderer.positionCount = 0;
            RecognizingResult result = spellCastingHandler.GetResult();

            spellCaster.CastBasedOnResult(result);
            spellCastingHandler.ResetHandler();
        }
    }

    public void OnDisable()
    {
        lineRenderer.positionCount = 0;
        spellCastingHandler.ResetHandler();
    }
}
