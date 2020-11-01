using UnityEngine;

public static class ElementTypeExtensions
{
    public static ElementType GetElementTypeByName(string name)
    {
        switch (name)
        {
            case "Fire":
                return ElementType.Fire;
            case "Water":
                return ElementType.Water;
            case "Air":
                return ElementType.Air;
            case "Earth":
                return ElementType.Earth;
            default:
                return ElementType.TrueDamage;
        }
    }

    public static Color GetElementTypeColor(this ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.Fire:
                return Color.red;
            case ElementType.Water:
                return Color.cyan;
            case ElementType.Air:
                return Color.white;
            case ElementType.Earth:
                return new Color32(244, 164, 96, 255);
            default:
                return Color.grey;
        }
    }
}
