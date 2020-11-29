using System.Collections;
using UnityEngine;

public class BalanceTest : MonoBehaviour
{
    public float dmg;
    public ElementType elementType;

    public ResistantTarget resistantTarget;
    public Fighter fighter;
    public Health health;
    public Resistant resistant;
    public PercentDamageModifier damageModifier;
    public DamageBlock damageBlock;

    public Fighter mock;

    private int turns = 0;
    private bool end = false;

    void Start()
    {
        fighter.SubscribeToDie(CheckResult);
        fighter.SetupForFight(mock);

        StartCoroutine(HpTest());
    }

    private IEnumerator HpTest()
    {
        while (!end)
        {
            Turn();

            yield return new WaitForSeconds(1);
        }   
    }

    private void Turn()
    {
        turns++;

        mock.Attack();

        resistantTarget.TakeDamage(dmg, elementType);
    }

    private void CheckResult()
    {
        end = true;
        Debug.Log("(" + gameObject.name + ") " + "R/E Turns: " + turns + "/" + ExpectedTurns());
    }

    private int ExpectedTurns()
    {
        float blockChance = health.blockChance / 100;

        float resistantDamage = resistant.CalculateDmg(dmg, elementType);
        float modifierDamage = damageModifier.CalculateDamage(resistantDamage);
        float blockDamage = modifierDamage - damageBlock.blockValue;
        float takeDamage = blockDamage >= 0 ? blockDamage : 0;

        float blockedDamage = takeDamage * blockChance;
        float normalDamage = modifierDamage * (1 - blockChance);

        return Mathf.CeilToInt(health.maxHp / (blockedDamage + normalDamage));
    }
}
