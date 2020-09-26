using UnityEngine;

public class PetModuleInputTester : MonoBehaviour
{
    public int unlockId = 0;

    [Header ("Components")]
    public PetUnlockerComponent petUnlockerComponent;
    public PetInitializerComponent petInitializerComponent;

    [Header("Interface Impls")]
    public AttackableTestImpl attackable;
    public HealableTestImpl healable;
    public ResetableTestImpl resetable;

    [Header("UI")]
    public PetTab petTab;
    private bool isTabActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isTabActive)
            {
                petTab.HideTab();
            }
            else
            {
                petTab.ShowTab();
            }

            isTabActive = !isTabActive;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            petUnlockerComponent.SetAvailablePet(unlockId);
            petUnlockerComponent.CollectPet();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            petInitializerComponent.SummonPet();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            attackable.Switch();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            healable.Switch();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            resetable.Switch();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            resetable.CastWhatever();
        }
    }
}
