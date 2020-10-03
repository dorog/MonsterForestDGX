using System;
using UnityEngine;

[Serializable]
public class Player : Fighter
{
    public BattleManager battleManager;
    public MagicCircleHandler magicCircleHandler;

    public Teleport teleport;

    public PlayerHealth playerHealth;

    private PatternRecognizerComponent spellManager;
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

    private bool isStopped = false;

    private void Start()
    {
        health.SetUpHealth();

        aliveMonstersManager = AliveMonstersManager.GetInstance();
        gatesManager = GateManager.GetInstance();

        gameEvents = GameEvents.GetInstance();
        gameEvents.BattleStartDelegateEvent += BattleStarted;
    }


    public void BattleStarted()
    {
        GoCall("BTS");
        //Stopped?.Invoke();

        leftHandCanvas.SetActive(true);
        rightHandCanvas.SetActive(true);

        playerHealth.Full();
    }

    //Rename it
    public void BattleEnd(int id, bool isMonster)
    {
        GoCall("BTE");
        //Go?.Invoke();

        playerHealth.BlockDown();

        leftHandCanvas.SetActive(false);
        rightHandCanvas.SetActive(false);

        magicCircleHandler.BattleEnd();

        Debug.Log("Comment: not save and add the win xp xp right now");
        //spellManager.Won();

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

        GoCall("Die");
        //Go?.Invoke();

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
        StopCall("Battle");
        //Stopped?.Invoke();

        battleManager.PlayerTurnEndDelegateEvent += DefTurn;
    }

    //Refactor
    public void MenuState()
    {
        if (!isStopped)
        {
            GoCall("MenuS");
            //Stopped?.Invoke();
        }
        else
        {
            GoCall("MenuG");
            //Go?.Invoke();
        }

        isStopped = !isStopped;
    }

    public void Run()
    {
        GoCall("Run");
        //Go?.Invoke();

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
        GoCall("PlayerDied");
        //Go?.Invoke();

        health.ResetHealth();
        teleport.TeleportToLastPosition();
    }

    public void FinishedTraining()
    {
        GoCall("FT");
        //Go?.Invoke();

        teleport.TeleportToLastPosition();

        magicCircleHandler.canAttack = false;

        playerHealth.BlockDown();

        spellGuide.ClearGuide();

        PlayerExperience playerExperience = PlayerExperience.GetInstance();
        playerExperience.AddExp(2000f, 1);

        DataManager.GetInstance().Won(playerExperience.GetExp());
    }

    private void GoCall(string name)
    {
        Debug.Log(name);
        Go?.Invoke();
    }

    private void StopCall(string name)
    {
        Debug.Log(name);
        Stopped?.Invoke();
    }
}
