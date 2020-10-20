using UnityEngine;

public class BattleLobby : MonoBehaviour
{
    public BattleManager battleManager;
    public ResistantValue water;
    public ResistantValue earth;
    public ResistantValue fire;
    public ResistantValue air;

    public GameObject petTab;
    public GameObject resistantTab;

    private GameEvents gameEvents;

    public GameObject battleLobbyUI;

    public BattleConnector battleConnector;

    public void Start()
    {
        gameEvents.BattleLobbyEnteredDelegateEvent += SetupUI;
    }

    public void StartBattle()
    {
        battleLobbyUI.SetActive(false);
        battleConnector.Fight();
    }

    private void SetupUI()
    {
        SetResistantValues(gameEvents.enemyResistant);
        SetPetTab(gameEvents.petEnable);
        SetResistantTab(gameEvents.resistantEnable);

        battleLobbyUI.SetActive(true);
    }

    private void SetPetTab(bool petEnable)
    {
        petTab.SetActive(petEnable);
    }

    private void SetResistantTab(bool resistantEnable)
    {
        resistantTab.SetActive(resistantEnable);
    }

    private void SetResistantValues(Resistant resistant)
    {
        water.SetValue(ElementType.Water.ToString(), resistant.water);
        earth.SetValue(ElementType.Earth.ToString(), resistant.earth);
        fire.SetValue(ElementType.Fire.ToString(), resistant.fire);
        air.SetValue(ElementType.Air.ToString(), resistant.air);
    }

    public void Run()
    {
        battleManager.WithdrawFromFight();

        battleLobbyUI.SetActive(false);
    }
}
