using System.Collections;
using UnityEngine;

public class MonsterAttackCommand : AbstractCommand
{
    [Range(0, 100)]
    public float[] extraAttackChances;

    public IAttack attack;

    private bool isCancelled = false;

    public Player player;

    protected override IEnumerator ExecuteCommand()
    {
        player.playerDiedDelegateEvent += CancelAttacking;

        isCancelled = false;

        float animationTime = Attack();

        yield return new WaitForSeconds(2 + animationTime);

        for (int i = 0; i < extraAttackChances.Length; i++)
        {
            float extraAttack = Random.Range(0, 101);

            if (!isCancelled && extraAttack <= extraAttackChances[i])
            {
                animationTime = Attack();

                yield return new WaitForSeconds(2 + animationTime);
            }
            else
            {
                break;
            }
        }

        player.playerDiedDelegateEvent -= CancelAttacking;
        Controller.FinishedTheCommand();
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
