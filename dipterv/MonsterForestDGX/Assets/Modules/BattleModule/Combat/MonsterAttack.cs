using UnityEngine;

public class MonsterAttack : IAttack
{
    public Animator animator;

    public MonsterAttackClass[] normalAttacks;
    public MonsterAttackClass[] hardAttacks;
    public MonsterAttackClass[] ultimateAttacks;

    public MonsterAttackChances attackChances;

    public Monster monster;

    public override float Attack()
    {
        monster.Attack();

        float random = Random.Range(1, 101);

        string animationKey;
        int animation;
        float time;

        if (random <= attackChances.normalAttakChance)
        {
            animation = GetOneRandomAnimation(normalAttacks);
            animationKey = normalAttacks[animation].animation;
            time = normalAttacks[animation].time;
            foreach(var attackGO in normalAttacks[animation].activate)
            {
                attackGO.SetActive(true);
            }
        }
        else if (random - attackChances.normalAttakChance <= attackChances.hardAttackChance)
        {
            animation = GetOneRandomAnimation(hardAttacks);
            animationKey = hardAttacks[animation].animation;
            time = hardAttacks[animation].time;
            foreach (var attackGO in hardAttacks[animation].activate)
            {
                attackGO.SetActive(true);
            }
        }
        else
        {
            animation = GetOneRandomAnimation(ultimateAttacks);
            animationKey = ultimateAttacks[animation].animation;
            time = ultimateAttacks[animation].time;
            foreach (var attackGO in ultimateAttacks[animation].activate)
            {
                attackGO.SetActive(true);
            }
        }

        animator.SetTrigger(animationKey);

        return time;
    }

    private int GetOneRandomAnimation(MonsterAttackClass[] animations)
    {
        int random = Random.Range(0, animations.Length);
        return random;
    }
}
