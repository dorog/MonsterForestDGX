using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatternShopManager
{
    void SubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method);
    void UnsubscibeToPatternDataLoadedEvent(Action<UiPatternData[]> method);
}
