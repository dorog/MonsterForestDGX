using System;

public interface IPressed
{
    void Activate();
    void Deactivate();
    void SubscribeToPressing(Action method);
    void UnsubscribeFromPressing(Action method);
    void SubscribeToPressed(Action method);
    void SubscribeToPressed(Action[] methods);
    void UnsubscribeFromPressed(Action method);
    void UnsubscribeFromPressed(Action[] methods);
    void SubscribeToReleased(Action method);
    void UnsubscribeFromReleased(Action method);
    void Reset();
}
