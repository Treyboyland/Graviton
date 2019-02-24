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
        public class PlayerHitWall : UnityEvent<bool> { }

        [Serializable]
        public class PointsReceivedAtPosition : UnityEvent<Vector3, Color> { }

        [Serializable]
        public class ScoreUpdated : UnityEvent<int> { }

        [Serializable]
        public class GrantInvincibility : UnityEvent<float> { }

        [Serializable]
        public class PlayerTakeDamage : UnityEvent { }

        [Serializable]
        public class PlayerSpeedUpdated : UnityEvent<float> { }

        [Serializable]
        public class ResetPlayerCombo : UnityEvent { }

        [Serializable]
        public class IncreasePlayerCombo : UnityEvent { }

        [Serializable]
        public class PlayerComboUpdated : UnityEvent<int> { }
    }

    /// <summary>
    /// Event that should be fired when the player gets awarded points
    /// </summary>
    public Events.PointsReceived OnPointsReceived;

    /// <summary>
    /// Event that should be fired when the player hits a wall
    /// </summary>
    public Events.PlayerHitWall OnPlayerHitWall;

    /// <summary>
    /// Event that should be sent with the position at which the player received points
    /// </summary>
    public Events.PointsReceivedAtPosition OnPointsReceivedAtPosition;

    /// <summary>
    /// Event sent when the player's score has been updated
    /// </summary>
    public Events.ScoreUpdated OnScoreUpdated;

    /// <summary>
    /// Event fired when the player should be granted invincibility
    /// </summary>
    public Events.GrantInvincibility OnGrantPlayerInvincibility;

    /// <summary>
    /// Event invoked when the player should take damage
    /// </summary>
    public Events.PlayerTakeDamage OnPlayerTakeDamage;

    /// <summary>
    /// Event invoked when the player's speed changes
    /// </summary>
    public Events.PlayerSpeedUpdated OnPlayerSpeedUpdated;

    /// <summary>
    /// Event fired when the player's combo level should be reset
    /// </summary>
    public Events.ResetPlayerCombo OnResetPlayerCombo;

    /// <summary>
    /// Event fired when the player's combo level should be increased
    /// </summary>
    public Events.IncreasePlayerCombo OnIncreasePlayerCombo;

    /// <summary>
    /// Event fired when the player combo has been updated
    /// </summary>
    public Events.PlayerComboUpdated OnPlayerComboUpdated;

    void Awake()
    {
        if (_instance != null && this != _instance)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }
}