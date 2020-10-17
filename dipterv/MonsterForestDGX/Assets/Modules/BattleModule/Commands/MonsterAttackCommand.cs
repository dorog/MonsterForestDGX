using System.Collections;
using UnityEngine;

public class MonsterAttackCommand : AbstractCommand
{
    [Range(0, 100)]
    public float[] extraAttackChances;

    public IAttack attack;

    private bool isCancelled = false;

    public Fighter enemy;
    public float attackDelay = 2f;

    protected override IEnumerator ExecuteCommand()
    {
        enemy.SubscribeToDie(CancelAttacking);

        isCancelled = false;

        float animationTime = Attack();

        yield return new WaitForSeconds(attackDelay + animationTime);

        for (int i = 0; i < extraAttackChances.Length; i++)
        {
            float extraAttack = Random.Range(1, 101);

            if (!isCancelled && extraAttack <= extraAttackChances[i])
            {
                animationTime = Attack();

                yield return new WaitForSeconds(attackDelay + animationTime);
            }
            else
            {
                break;
            }
        }

        enemy.UnsubscribeToDie(CancelAttacking);
    }

    private float Attack()
    {
        return attack.Attack();
    }

    private void CancelAttacking()
    {
        isCancelled = true;
    }
}
