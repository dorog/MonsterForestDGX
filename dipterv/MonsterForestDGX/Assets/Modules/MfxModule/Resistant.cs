﻿using UnityEngine;

[CreateAssetMenu(fileName = "new Resistant", menuName = "Resistant")]
public class Resistant
{
    [Range(-100, 100)]
    public int water = 0;
    [Range(-100, 100)]
    public int earth = 0;
    [Range(-100, 100)]
    public int fire = 0;
    [Range(-100, 100)]
    public int air = 0;

    public float CalculateDmg(float dmg, ElementType elementType)
    {
        float resistant = GetResistant(elementType);

        return dmg * (1 - (resistant / 100));
    }

    public float GetResistant(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.Water:
                return water;
            case ElementType.Earth:
                return water;
            case ElementType.Fire:
                return fire;
            case ElementType.Air:
                return air;
            default:
                return 0;
        }
    }
}
