using System.Collections;
using UnityEngine;

public class MonsterAttackCommand : AbstractCommand
{
    [Range(0, 100)]
    public float[] extraAttackChances;

    public IAttack attack;

    protected override IEnumerator ExecuteCommand()
    {
        float animationTime = Attack();

        yield return new WaitForSeconds(2 + animationTime);

        for (int i = 0; i < extraAttackChances.Length; i++)
        {
            float extraAttack = Random.Range(0, 101);

            if (extraAttack <= extraAttackChances[i])
            {
                animationTime = Attack();

                yield return new WaitForSeconds(2 + animationTime);
            }
            else
            {
                break;
            }
        }

        Controller.FinishedTheCommand();
    }

    private float Attack()
    {
        return attack.Attack();
    }
}
