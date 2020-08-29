using System;
using UnityEngine;

[Serializable]
public class Player : Fighter
{
    public MagicCircleHandler magicCircleHandler;

    public BattleManager battleManager;

    public Teleport teleport;

    public bool InLobby = false;
    public bool InBattle = false;
    public bool InMenu = false;

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
        InLobby = false;

        InBattle = true;

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
        InBattle = false;
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
        leftHandCanvas.SetActive(false);
        rightHandCanvas.SetActive(false);

        magicCircleHandler.Die();
        battleManager.PlayerDied();
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
        this.petEnable = petEnable;

        InLobby = true;
        this.battleManager = battleManager;
        battleLobbyUI.battleManager = battleManager;
        battleLobbyUI.SetResistantValues(monsterResistant);
        battleLobbyUI.SetPetTab(petEnable);
        battleLobbyUI.SetResistantTab(resistantEnable);

        battleLobbyUI.gameObject.SetActive(true);
    }

    public bool CanMove()
    {
        return !(InLobby || InBattle || InMenu);
    }

    public void MenuState(bool state)
    {
        InMenu = state;
    }

    public void Run()
    {
        InLobby = false;
        //teleport.TeleportToLastPosition();
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
        InBattle = false;

        if (petGO != null)
        {
            Destroy(petGO);
        }

        health.ResetHealth();
        teleport.TeleportToDefaultLocation();
    }

    public bool CanAttack()
    {
        return magicCircleHandler.canAttack;
    }

    public void FinishedTraining()
    {
        magicCircleHandler.canAttack = false;
        InBattle = false;

        playerHealth.BlockDown();

        teleport.TeleportToLastPosition();
    }
}
