using System.Collections.Generic;
using UnityEngine;

public class SpellManager : SingletonClass<SpellManager>
{
    private PlayerExperience playerExperience;

    public SpellPattern SpellPattern;

    private readonly List<ISpellPattern> attackPatterns = new List<ISpellPattern>();

    public Transform attackParent;

    private float lastCoverage = 0;
    private float castedCoverage = 0;

    public GameObject castParent;
    public CastEffect[] castEffects;

    public SpellsUI spellsUI;

    public Transform castEffectTransformReference;

    private List<CoverageResult> coverageResults = new List<CoverageResult>();

    private void Awake()
    {
        Init(this);
    }

    void Start()
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.spellLevelChangedEvent += SpellLeveledUp;

        CreatePatterns(dataManager.GetBasePatterns(), dataManager.GetBasePatternLevels(), attackPatterns, attackParent);
        spellsUI.SetupUI(attackPatterns);

        playerExperience = PlayerExperience.GetInstance();

    }

    private void CreatePatterns(List<BasePatternSpell> BasePaternSpells, List<int> levels, List<ISpellPattern> SpellPatterns, Transform parent, float extraHeigh = 0)
    {
        for (int i = 0; i < BasePaternSpells.Count; i++)
        {
            CreateSpellPattern(BasePaternSpells[i], levels[i], SpellPatterns, extraHeigh);
        }
    }

    private void CreateSpellPattern(BasePatternSpell basePaternSpell, int level, List<ISpellPattern> SpellPatterns, float extraHeigh = 0)
    {
        List<Vector2> points = new List<Vector2>();
        List<SpellPatternPoint> spellPatternPoints = basePaternSpell.SpellPatternPoints.GetPoints();
        for (int i = 0; i < spellPatternPoints.Count; i++)
        {
            points.Add(spellPatternPoints[i].Point);
        }

        PatternFormula spellPattern = new PatternFormula(points, basePaternSpell.icon)
        {
            ElementType = basePaternSpell.SpellPatternPoints.attackType,
            level = level,
            Spells = basePaternSpell.GetSpells(),
            RequiredExps = basePaternSpell.GetRequiredExps()
        };
        SpellPatterns.Add(spellPattern);
    }

    public void Guess(Vector2 point)
    {
        foreach (var spellPattern in attackPatterns)
        {
            spellPattern.Guess(point);
        }
    }

    public SpellResult GetSpell()
    {
        coverageResults.Clear();

        int index = -1;
        float max = -1;
        for (int i = 0; i < attackPatterns.Count; i++)
        {
            float result = attackPatterns[i].GetResult();
            float minCoverage = attackPatterns[i].GetMinCoverage();

            coverageResults.Add(new CoverageResult(attackPatterns[i].GetElementType().ToString(), result, minCoverage));

            if ((minCoverage <= result) && (result > max))
            {
                index = i;
                max = result;
            }
        }

        castedCoverage = max;

        if (index != -1)
        {
            playerExperience.AddExp(ExpType.Cast, max);
            SpellResult spellResult = new SpellResult
            {
                id = index,
                spell = attackPatterns[index].GetSpell(),
                coverage = max,
                cooldown = attackPatterns[index].GetCooldown()
            };

            ElementType elementType = attackPatterns[index].GetElementType();
            GameObject castEffect = null;
            for(int i = 0; i < castEffects.Length; i++)
            {
                if(elementType == castEffects[i].ElementType)
                {
                    castEffect = castEffects[i].gameObject;
                    break;
                }
            }
            if(castEffect != null)
            {
                Instantiate(castEffect, castEffectTransformReference.position, castEffectTransformReference.rotation);
            }

            return spellResult;
        }
        else
        {
            return null;
        }
    }

    public void ResetSpells()
    {
        foreach (var spellPattern in attackPatterns)
        {
            spellPattern.Reset();
        }
    }

    public void Won()
    {
        playerExperience.AddExp(ExpType.Kill, lastCoverage);

        DataManager.GetInstance().Won(attackPatterns, playerExperience.GetExp());
    }

    public float GetCastedCoverage()
    {
        return castedCoverage;
    }

    public List<CoverageResult> GetCoverageResults()
    {
        return coverageResults;
    }

    public void AddXpForHit(float coverage)
    {
        lastCoverage = coverage;
        playerExperience.AddExp(ExpType.Hit, coverage);
    }

    public void SpellLeveledUp(int id)
    {
        attackPatterns[id].LevelUp();
    }

    public List<bool> GetAttackSpellsState()
    {
        List<bool> states = new List<bool>();

        foreach(var attackSpell in attackPatterns)
        {
            if(attackSpell.GetLevelValue() == 0)
            {
                states.Add(false);
            }
            else
            {
                states.Add(true);
            }
        }

        return states;
    }

    public List<string> GetAttackSpellNames()
    {
        List<string> names = new List<string>();

        foreach (var attackSpell in attackPatterns)
        {
            if (attackSpell.GetLevelValue() != 0)
            {
                names.Add(attackSpell.GetElementType().ToString());
            }
        }

        return names;
    }

    public int GetFilteredAttackSpellState(ElementType elementType)
    {
        for (int i = 0; i < attackPatterns.Count; i++)
        {
            if (attackPatterns[i].GetLevelValue() != 0 && attackPatterns[i].GetElementType() == elementType)
            {
                return i;
            }
        }

        return -1;
    }

    public BasePatternSpell GetSpellPoints(string name)
    {
        var basePatterns = DataManager.GetInstance().GetBasePatterns();

        if(name == "No")
        {
            return null;
        }
        else
        {
            ElementType type = ElementTypeExtensions.GetElementTypeByName(name);

            for(int i = 0; i < basePatterns.Count; i++)
            {
                if(basePatterns[i].elementType == type)
                {
                    return basePatterns[i];
                }
            }

            return null;
        }
    }
}
