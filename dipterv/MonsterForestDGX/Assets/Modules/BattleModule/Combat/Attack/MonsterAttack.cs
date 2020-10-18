using UnityEngine;

public class MonsterAttack : IAttack
{
    public Animator animator;

    public MonsterAttackChance[] attackChances;

    public Monster monster;

    private int allChance = -1;

    public override float Attack()
    {
        monster.Attack();

        float random = Random.Range(1, GetAllChance());

        string animationKey = "";
        float time = 0;

        int chance = 0;
        foreach(var attackChance in attackChances)
        {
            chance += attackChance.chance;
            if (random <= chance)
            {
                int animation = GetOneRandomAnimation(attackChance.monsterAttacks);

                MonsterAttackData selected = attackChance.monsterAttacks[animation];
                animationKey = selected.animation;
                time = selected.time;
                foreach (var attackGO in selected.activate)
                {
                    attackGO.SetActive(true);
                }

                break;
            }
        }

        animator.SetTrigger(animationKey);

        return time;
    }

    private int GetAllChance()
    {
        if(allChance != -1)
        {
            return allChance;
        }

        allChance = 1;
        foreach (var attackChance in attackChances)
        {
            allChance += attackChance.chance; 
        }

        return allChance;
    }

    private int GetOneRandomAnimation(MonsterAttackData[] animations)
    {
        int random = Random.Range(0, animations.Length);
        return random;
    }
}
