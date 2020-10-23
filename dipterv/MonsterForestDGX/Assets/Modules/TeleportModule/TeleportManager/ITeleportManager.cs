using System;
using System.Collections.Generic;

public interface ITeleportManager<T>
{
    void Load();
    void SubscribeToLoad(Action<List<T>> method);
    void SubscribeToChanged(Action<int> method);
    void SubscribeToTargetLocationChanged(Action<int> method);
}
