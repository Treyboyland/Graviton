using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListenerGeneric<T> : MonoBehaviour
{
    [SerializeField]
    GameEventGeneric<T> eventToListenFor;

    public UnityEvent<T> Response;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        eventToListenFor.AddListener(this);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        eventToListenFor.RemoveListener(this);
    }
}
