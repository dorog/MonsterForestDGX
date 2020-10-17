
public enum ElementType
{
    TrueDamage, Fire, Water, Air, Earth
}

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
}
