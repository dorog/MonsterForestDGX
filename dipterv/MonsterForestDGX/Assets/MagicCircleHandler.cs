using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MagicCircleHandler : MonoBehaviour
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

    public Text feedback;

    public float magicCircleExtraDistance = 2;


    public void Start()
    {
        magicCircleInput = KeyBindingManager.GetInstance().magicCircleInput;
        magicCircleInput.SubscribeToPressed(new Action[] { MagicCircleStatePositive, MagicCircleStateNegativ });

        BattleEvents.GetInstance().SubscribeEvents(Fighting, Exploring);
    }

    private void Fighting(BattleManager battleManager)
    {
        battleManager.PlayerTurnStartDelegateEvent += AttackTurn;
        battleManager.MonsterTurnStartDelegateEvent += DefTurn;
    }

    private void Exploring(BattleManager battleManager)
    {
        battleManager.PlayerTurnStartDelegateEvent -= AttackTurn;
        battleManager.MonsterTurnStartDelegateEvent -= DefTurn;

        magicCircleInput.Deactivate();
    }

    public void ResetCooldown()
    {
        resetedCd = true;
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
}
