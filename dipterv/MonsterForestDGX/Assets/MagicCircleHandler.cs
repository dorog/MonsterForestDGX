using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MagicCircleHandler : MonoBehaviour, IResetable
{
    public bool canAttack = false;

    public GameObject magicCircle;

    public Text coolDown;
    private bool resetedCd = false;

    public Player player;

    public delegate void CastSpellDelegate();
    public CastSpellDelegate successCastSpellDelegateEvent;
    public CastSpellDelegate failedCastSpellDelegateEvent;

    public GameObject hand;

    public IPressed magicCircleInput;

    public float magicCircleExtraDistance = 2;

    public BattleManager battleManager;

    public ParticleSystem cooldownParticleSystemEffect;

    public GameEvents gameEvents;

    private CooldownResetPetAbility cooldownReset;

    public void Start()
    {
        magicCircleInput = KeyBindingManager.GetInstance().magicCircleInput;
        magicCircleInput.SubscribeToPressed(new Action[] { MagicCircleStatePositive, MagicCircleStateNegativ });

        gameEvents.BattleStartDelegateEvent += Fighting;
        gameEvents.BattleEndDelegateEvent += Exploring;
    }

    private void Fighting()
    {
        battleManager.PlayerTurnStartDelegateEvent += AttackTurn;
        battleManager.MonsterTurnStartDelegateEvent += DefTurn;
    }

    private void Exploring()
    {
        battleManager.PlayerTurnStartDelegateEvent -= AttackTurn;
        battleManager.MonsterTurnStartDelegateEvent -= DefTurn;

        magicCircleInput.Deactivate();
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

    public void Def()
    {
        magicCircle.SetActive(false);
    }

    public void BattleEnd()
    {
        canAttack = false;
        magicCircle.SetActive(false);

        ClearDelegates();
    }

    public void CastSpell(SpellResult spellResult, BattleManager battleManager)
    {
        GameObject spell = Instantiate(spellResult.spell, magicCircle.transform.position, magicCircle.transform.rotation);
        SpellAttack spellAttack = spell.GetComponent<SpellAttack>();
        spellAttack.coverage = spellResult.coverage;

        spellAttack.SetBattleManager(battleManager);

        SetUpCoolDown(spellResult.cooldown);

        magicCircleInput.Reset();
        magicCircle.SetActive(false);
    }

    private IEnumerator Countdown(float cd)
    {
        float duration = cd;
        while (duration > 0 && !resetedCd)
        {
            coolDown.text = duration.ToString();
            duration -= Time.deltaTime;

            yield return null;
        }

        coolDown.text = "Ready";

        resetedCd = false;
    }

    private void SetUpCoolDown(float cd)
    {
        successCastSpellDelegateEvent?.Invoke();

        if (!resetedCd)
        {
            coolDown.text = cd.ToString();
            StartCoroutine(Countdown(cd));
        }
        else
        {
            resetedCd = false;
        }
    }

    //TODO: Rename
    public void Die()
    {
        //Necessary?
        magicCircle.SetActive(false);
    }

    public void DefTurn()
    {
        magicCircleInput.Deactivate();

        canAttack = false;
        magicCircle.SetActive(false);
    }

    public void AttackTurn()
    {
        magicCircleInput.Activate();

        canAttack = true;
        magicCircle.SetActive(false);
    }

    private void ClearDelegates()
    {
        if (successCastSpellDelegateEvent != null)
        {
            Delegate[] delegates = successCastSpellDelegateEvent.GetInvocationList();
            foreach (Delegate d in delegates)
            {
                successCastSpellDelegateEvent -= (CastSpellDelegate)d;
            }
        }
    }

    public Vector3 GetPosition()
    {
        return hand.transform.position;
    }

    public Transform GetTransform()
    {
        return magicCircle.transform;
    }

    public void ResetAction()
    {
        resetedCd = true;
        cooldownParticleSystemEffect.Play();
    }

    public void AddCooldownRef(CooldownResetPetAbility cooldownResetPetAbility)
    {
        if(cooldownReset != null)
        {
            cooldownReset.Deactivate();
            successCastSpellDelegateEvent -= cooldownReset.ResetCooldown;
        }

        cooldownReset = cooldownResetPetAbility;
        successCastSpellDelegateEvent += cooldownResetPetAbility.ResetCooldown;
        cooldownResetPetAbility.Activate();
    }
}
