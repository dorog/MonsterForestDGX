using System;

public interface ITeleporterManager : ITeleportManager<ITeleporterPoint>
{
    void LocationChanged(int id);
    void SubscribeToLastPositionIdLoad(Action<int> method);
}
