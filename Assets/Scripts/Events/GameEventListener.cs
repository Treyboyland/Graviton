using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    GameEvent eventToListenFor;

    public UnityEvent Response;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        eventToListenFor.AddListener(this);
    }

    void OnDisable()
    {
        eventToListenFor.RemoveListener(this);
    }
}
