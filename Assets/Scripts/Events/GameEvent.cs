using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent-None-On", menuName = "Events/No Params", order = -1)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public virtual void Invoke()
    {
        foreach (var listener in listeners)
        {
            if (listener != null)
            {
                listener.Response.Invoke();
            }
        }
    }
}
