using System;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpellCastingHandler spellCastingHandler;
    public Player player;

    public float scale = 0.1f;

    public MagicCircleHandler magicCircleHandler;

    public IPressed drawingInput;

    public Transform hand;

    private BattleManager battleManager;

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

        battleManager = gameEvents.battleManager;
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
                Transform flat = magicCircleHandler.transform;

                Vector3 relativPosition = hand.position - flat.position;
                Vector3 projected = Vector3.ProjectOnPlane(relativPosition, flat.forward);
                Vector3 flattenedVector = flat.position + projected;

                Vector2 guess = GetGuess(flattenedVector, flat) * 200;

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
            SpellResult spellResult = spellCastingHandler.GetResult();
            if (spellResult == null)
            {
                magicCircleHandler.CastFailed();
            }
            else
            {
                magicCircleHandler.CastSpell(spellResult);
            }
            spellCastingHandler.ResetHandler();
        }
    }

    public void OnDisable()
    {
        lineRenderer.positionCount = 0;
        spellCastingHandler.ResetHandler();
    }
}
