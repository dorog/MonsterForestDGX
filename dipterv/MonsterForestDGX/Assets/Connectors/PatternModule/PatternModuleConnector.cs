using UnityEngine;

public class PatternModuleConnector : MonoBehaviour
{
    [Header("Managers")]
    public PatternManager patternManager;

    [Header("Components")]
    public PatternRecognizerComponent patternRecognizerComponent;
    public PatternShopComponent patternShopComponent;
    public MfxTraningCampPatternComponent traningCampPatternComponent;
    public PatternInfoComponent patternInfoComponent;

    [Header("Dependencies (Not Component)")]
    public MfxPatternDataHandler patternDataHandler;
    public MfxPatternCurrencyHandler currencyHandler;

    void Start()
    {
        patternManager.patternDataHandler = patternDataHandler;
        patternShopComponent.currencyHandler = currencyHandler;

        patternRecognizerComponent.Init(patternManager);
        patternShopComponent.Init(patternManager);
        traningCampPatternComponent.Init(patternManager);
        patternInfoComponent.Init(patternManager);

        patternManager.LoadData();
        currencyHandler.LoadQuantity();
    }
}
