using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventGeneric<T> : GameEvent
{
    public T Value;

    protected List<GameEventListenerGeneric<T>> listeners = new List<GameEventListenerGeneric<T>>();

    public void AddListener(GameEventListenerGeneric<T> listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListenerGeneric<T> listener)
    {
        listeners.Remove(listener);
    }

    public override void Invoke()
    {
        foreach (var listener in listeners)
        {
            if (listener != null)
            {
                listener.Response.Invoke(Value);
            }

        }
    }

    public void Invoke(T value)
    {
        Value = value;
        Invoke();
    }
}
