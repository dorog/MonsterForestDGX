using UnityEngine;

public class CastEffectHandler : MonoBehaviour
{
    public CastEffect[] castEffects;
    public Transform castEffectTransformReference;

    public void CastSpellEffect(ElementType elementType)
    {
        GameObject castEffect = null;
        for (int i = 0; i < castEffects.Length; i++)
        {
            if (elementType == castEffects[i].ElementType)
            {
                castEffect = castEffects[i].gameObject;
                break;
            }
        }
        if (castEffect != null)
        {
            Instantiate(castEffect, castEffectTransformReference.position, castEffectTransformReference.rotation);
        }
    }
}
