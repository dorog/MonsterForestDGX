using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICastablePattern : IPattern
{
    GameObject GetSpell();
    ElementType GetElementType();
    int GetLevelValue();
    float GetCooldown();
}
