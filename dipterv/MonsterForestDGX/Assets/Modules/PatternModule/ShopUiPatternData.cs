
public class ShopUiPatternData : PatternData
{
    public IShopUiPattern ShopUiPattern { get; set; }

    public override IPattern GetPattern()
    {
        return ShopUiPattern;
    }
}
