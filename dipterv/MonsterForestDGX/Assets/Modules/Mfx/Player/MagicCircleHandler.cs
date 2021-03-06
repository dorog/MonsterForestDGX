﻿using System;
using UnityEngine;

public class MagicCircleHandler : MonoBehaviour, IResetable
{
    public GameObject magicCircle;

    public event Action SuccessCastSpellDelegateEvent;
    public event Action<float> SuccessCastSpellWithCoverage;
    public event Action FailedCastSpellDelegateEvent;

    public GameObject hand;

    public float magicCircleExtraDistance = 2;

    public ParticleSystem cooldownParticleSystemEffect;

    //TODO: Add connector
    private CooldownResetPetAbility cooldownReset;
    public CastEffectHandler castEffectHandler;

    public IPressed magicCircleInput;
    public ISpellHandler spellHandler;
    public ICooldownShower cooldownShower;

    public KeyBindingManager keyBindingManager;
    public BattleManager battleManager;
    public ExperienceManager experienceManager;

    public void Start()
    {
        magicCircleInput = keyBindingManager.magicCircleInput;
        magicCircleInput.SubscribeToPressed(new Action[] { MagicCircleStatePositive, MagicCircleStateNegativ });
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

    public void CastSpell(SpellResult spellResult)
    {
        SpellData spellData = spellHandler.GetSpellData(spellResult.Index);
        GameObject spell = Instantiate(spellData.Spell, magicCircle.transform.position, magicCircle.transform.rotation);
        SpellAttack spellAttack = spell.GetComponent<SpellAttack>();
        spellAttack.coverage = spellResult.Coverage;

        spellAttack.SetBattleManager(battleManager, experienceManager);

        cooldownShower.SetUpCoolDown(spellData.Cooldown);
        //Other option: Say: good, lame, awful etc
        SuccessCastSpellWithCoverage?.Invoke(spellResult.Coverage);
        SuccessCastSpellDelegateEvent?.Invoke();

        magicCircleInput.Reset();
        magicCircle.SetActive(false);

        castEffectHandler.CastSpellEffect(spellData.ElementType);
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

    public void ResetAction()
    {
        cooldownShower.ResetCoolDown();
        cooldownParticleSystemEffect.Play();
    }

    public void AddCooldownRef(CooldownResetPetAbility cooldownResetPetAbility)
    {
        if(cooldownReset != null)
        {
            cooldownReset.Deactivate();
            SuccessCastSpellDelegateEvent -= cooldownReset.ResetCooldown;
        }

        cooldownReset = cooldownResetPetAbility;

        if(cooldownResetPetAbility != null)
        {
            SuccessCastSpellDelegateEvent += cooldownResetPetAbility.ResetCooldown;
            cooldownResetPetAbility.Activate();
        }
    }
}
