using System;
using UnityEngine;

[Serializable]
public class Player : Fighter
{
    public BattleManager battleManager;
    public MagicCircleHandler magicCircleHandler;

    public Teleport teleport;

    public PlayerHealth playerHealth;

    private SpellManager spellManager;
    private AliveMonstersManager aliveMonstersManager;
    private GateManager gatesManager;

    public BattleLobby battleLobbyUI;

    public SpellGuideDrawer spellGuide;

    public GameEvents gameEvents;

    public event Action Stopped;
    public event Action Go;

    [Header("UI")]
    public GameObject leftHandCanvas;
    public GameObject rightHandCanvas;

    private void Start()
    {
        health.SetUpHealth();

        aliveMonstersManager = AliveMonstersManager.GetInstance();
        gatesManager = GateManager.GetInstance();
        spellManager = SpellManager.GetInstance();

        gameEvents = GameEvents.GetInstance();
        gameEvents.BattleStartDelegateEvent += BattleStarted;
    }


    public void BattleStarted()
    {
        Stopped?.Invoke();

        leftHandCanvas.SetActive(true);
        rightHandCanvas.SetActive(true);

        playerHealth.Full();
    }

    //Rename it
    public void BattleEnd(int id, bool isMonster)
    {
        Go?.Invoke();

        playerHealth.BlockDown();

        leftHandCanvas.SetActive(false);
        rightHandCanvas.SetActive(false);

        magicCircleHandler.BattleEnd();

        spellManager.Won();

        if (isMonster)
        {
            aliveMonstersManager.Won(id);
        }
        else
        {
            gatesManager.Won(id);
        }
    }

    public override void Die()
    {
        leftHandCanvas.SetActive(false);
        rightHandCanvas.SetActive(false);

        magicCircleHandler.Die();
        battleManager.PlayerDied();

        Go?.Invoke();

        base.Die();
    }

    public void DefTurn()
    {
        magicCircleHandler.DefTurn();
    }

    public void AttackTurn()
    {
        magicCircleHandler.AttackTurn();
    }

    public void Battle()
    {
        Stopped?.Invoke();

        battleManager.PlayerTurnEndDelegateEvent += DefTurn;
    }

    //Refactor
    public void MenuState(bool state)
    {
        if (state)
        {
            Stopped?.Invoke();
        }
        else
        {
            Go?.Invoke();
        }
    }

    public void Run()
    {
        Go?.Invoke();

        teleport.TeleportToLastPosition();
    }

    public void CastSpell(SpellResult spellResult)
    {
        magicCircleHandler.CastSpell(spellResult, battleManager);
    }

    public void FailedSpell()
    {
        magicCircleHandler.failedCastSpellDelegateEvent?.Invoke();
    }

    public MagicCircleHandler GetMagicCircleHandler()
    {
        return magicCircleHandler;
    }

    public void PlayerDied()
    {
        Go?.Invoke();

        health.ResetHealth();
        teleport.TeleportToLastPosition();
    }

    public void FinishedTraining()
    {
        Go?.Invoke();

        teleport.TeleportToLastPosition();

        magicCircleHandler.canAttack = false;

        playerHealth.BlockDown();

        spellGuide.ClearGuide();
    }
}
