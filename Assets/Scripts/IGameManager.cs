using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class BaseGameManager : MonoBehaviour
{

    static BaseGameManager _instance;

    public static BaseGameManager Manager
    {
        get
        {
            return _instance;
        }
    }

    public class Events
    {
        /// <summary>
        /// Event fired when the player gets points
        /// </summary>
        /// <typeparam name="int"></typeparam>
        [Serializable]
        public class PointsReceived : UnityEvent<int> { }

        /// <summary>
        /// Event fired when the player hits a wall
        /// </summary>
        [Serializable]
        public class PlayerHitWall : UnityEvent { }
    }

    /// <summary>
    /// Event that should be fired when the player gets awarded points
    /// </summary>
    public Events.PointsReceived OnPointsReceived;

    /// <summary>
    /// Event that should be fired when the player hits a wall
    /// </summary>
    public Events.PlayerHitWall OnPlayerHitWall;

    void Awake()
    {
        if(_instance != null && this != _instance)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }
}