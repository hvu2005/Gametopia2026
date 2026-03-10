using System;

public class EventEmitter
{
    public void On<T>(Enum eventType, Action<T> callback)
    {
        EventBus.On(eventType, callback);
    }

    public void Off<T>(Enum eventType, Action<T> callback)
    {
        EventBus.Off(eventType, callback);
    }

    protected void Emit<T>(Enum eventType, T args)
    {
        EventBus.Emit(eventType, args);
    }
}