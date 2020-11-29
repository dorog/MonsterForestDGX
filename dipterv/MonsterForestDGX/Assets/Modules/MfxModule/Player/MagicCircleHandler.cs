using System;
using System.Collections;
using UnityEngine;

public class MagicCircleHandler : MfxResetable
{
    public GameObject magicCircle;
    public GameObject circle;
    private bool reseted = false;

    public event Action SuccessCastSpellDelegateEvent;
    public event Action<float> SuccessCastSpellWithCoverage;
    public event Action FailedCastSpellDelegateEvent;

    public GameObject hand;

    public float magicCircleExtraDistance = 2;

    public ParticleSystem cooldownParticleSystemEffect;

    public CastEffectHandler castEffectHandler;

    public IPressed magicCircleInput;
    public ISpellHandler spellHandler;
    public ICooldownShower cooldownShower;

    public KeyBindingManager keyBindingManager;
    public RoundHandler roundHandler;
    public BattleManager battleManager;
    public GameEvents gameEvents;

    public void Start()
    {
        magicCircleInput = keyBindingManager.magicCircleInput;
        magicCircleInput.SubscribeToPressed(new Action[] { MagicCircleStatePositive, MagicCircleStateNegativ });

        gameEvents.BattleEndDelegateEvent += Explore;

        roundHandler.SubscribeToStartTurn(AttackTurn);
        roundHandler.SubscribeToEndTurn(DefTurn);
    }

    private void Explore()
    {
        DisableCasting();
    }

    private void MagicCircleStatePositive()
    {
        magicCircle.transform.position = hand.transform.position + hand.transform.forward * magicCircleExtraDistance;
        magicCircle.transform.rotation = hand.transform.rotation;
        magicCircle.SetActive(true);
    }

    private void MagicCircleStateNegativ()
    {
        magicCircle.SetActive(false);
    }

    public void CastSpell(RecognizingResult spellResult)
    {
        SpellData spellData = spellHandler.GetSpellData(spellResult.Index);
        GameObject spell = Instantiate(spellData.Spell, magicCircle.transform.position, magicCircle.transform.rotation);
        SpellAttack spellAttack = spell.GetComponent<SpellAttack>();
        spellAttack.coverage = spellResult.Coverage;

        spellAttack.SetBattleManager(roundHandler, battleManager);

        cooldownShower.SetUpCoolDown(spellData.Cooldown);
        StartCoroutine(EnableCircle(spellData.Cooldown));

        //Other option: Say: good, lame, awful etc
        SuccessCastSpellWithCoverage?.Invoke(spellResult.Coverage);
        SuccessCastSpellDelegateEvent?.Invoke();

        magicCircleInput.ResetPressed();
        magicCircle.SetActive(false);

        castEffectHandler.CastSpellEffect(spellData.ElementType);
    }

    private IEnumerator EnableCircle(float time)
    {
        circle.SetActive(false);

        reseted = false;
        float remainingTime = time;
        while(remainingTime > 0 && !reseted)
        {
            remainingTime -= Time.deltaTime;

            yield return null;
        }

        circle.SetActive(true);
    }

    public void CastFailed()
    {
        FailedCastSpellDelegateEvent?.Invoke();
    }

    public void DefTurn()
    {
        magicCircleInput.Deactivate();
        magicCircle.SetActive(false);
    }

    public void AttackTurn()
    {
        magicCircleInput.Activate();
        magicCircle.SetActive(false);
    }

    public void DisableCasting()
    {
        magicCircleInput.Deactivate();
        magicCircle.SetActive(false);
    }

    public override void ResetAction()
    {
        cooldownShower.ResetCoolDown();

        cooldownParticleSystemEffect.Play();
    }

    public override void SubscribeToReset(Action method)
    {
        SuccessCastSpellDelegateEvent += method;
    }

    public override void UnsubscribeToReset(Action method)
    {
        SuccessCastSpellDelegateEvent -= method;
    }
}
