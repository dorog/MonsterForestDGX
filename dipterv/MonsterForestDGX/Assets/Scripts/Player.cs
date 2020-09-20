using System;
using UnityEngine;

[Serializable]
public class Player : Fighter
{
    public BattleManager battleManager;
    public MagicCircleHandler magicCircleHandler;

    public Teleport teleport;

    public PlayerHealth playerHealth;

    public Color DefenseGridColor;
    public Color AttackGridColor;
    public SpriteRenderer grid;

    private GameObject petGO;

    private PetManager petManager;
    private SpellManager spellManager;
    private AliveMonstersManager aliveMonstersManager;
    private GateManager gatesManager;

    public BattleLobby battleLobbyUI;

    public ParticleSystem cooldownParticleSystemEffect;

    private bool petEnable = true;

    public SpellGuideDrawer spellGuide;

    public delegate void PlayerDiedDelegate();
    public PlayerDiedDelegate playerDiedDelegateEvent;

    public BattleEvents battle;

    public event Action Stopped;
    public event Action Go;

    [Header("UI")]
    public GameObject leftHandCanvas;
    public GameObject rightHandCanvas;


    private void Start()
    {
        health.SetUpHealth();

        petManager = PetManager.GetInstance();
        aliveMonstersManager = AliveMonstersManager.GetInstance();
        gatesManager = GateManager.GetInstance();
        spellManager = SpellManager.GetInstance();
    }


    public void BattleStarted()
    {
        Stopped?.Invoke();

        leftHandCanvas.SetActive(true);
        rightHandCanvas.SetActive(true);

        playerHealth.Full();

        if (petEnable)
        {
            GameObject playerPet = petManager.GetPet();
            if (playerPet != null)
            {
                petGO = Instantiate(playerPet, battleManager.GetPetPosition(), transform.rotation);

                Pet pet = petGO.GetComponent<Pet>();
                pet.AddPlayer(this);
            }
        }
    }

    //Rename it
    public void BattleEnd(int id, bool isMonster)
    {
        Go?.Invoke();

        playerHealth.BlockDown();

        leftHandCanvas.SetActive(false);
        rightHandCanvas.SetActive(false);

        magicCircleHandler.BattleEnd();

        if(petGO != null)
        {
            Destroy(petGO);
        }

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
        playerDiedDelegateEvent?.Invoke();

        leftHandCanvas.SetActive(false);
        rightHandCanvas.SetActive(false);

        magicCircleHandler.Die();
        battleManager.PlayerDied();

        Go?.Invoke();
    }

    public void DefTurn()
    {
        grid.color = DefenseGridColor;
        magicCircleHandler.DefTurn();
    }

    public void AttackTurn()
    {
        //grid.color = AttackGridColor;
        magicCircleHandler.AttackTurn();
    }

    public void Battle(BattleManager battleManager, Resistant monsterResistant, bool petEnable, bool resistantEnable)
    {
        Stopped?.Invoke();

        this.petEnable = petEnable;

        this.battleManager = battleManager;
        battleLobbyUI.battleManager = battleManager;
        battleLobbyUI.SetResistantValues(monsterResistant);
        battleLobbyUI.SetPetTab(petEnable);
        battleLobbyUI.SetResistantTab(resistantEnable);

        battleLobbyUI.gameObject.SetActive(true);

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

    public ParticleSystem GetCooldownEffect()
    {
        return cooldownParticleSystemEffect;
    }

    public void Died()
    {
        Go?.Invoke();

        if (petGO != null)
        {
            Destroy(petGO);
        }

        health.ResetHealth();
        teleport.TeleportToLastPosition();
    }

    public bool CanAttack()
    {
        return magicCircleHandler.canAttack;
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
