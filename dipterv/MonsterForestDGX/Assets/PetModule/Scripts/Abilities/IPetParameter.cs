using System;

public interface IPetParameter
{
    void SubscribeToEvents(Action activate, Action deactivate);
    void UnsubscribeToEvents(Action activate, Action deactivate);
}
