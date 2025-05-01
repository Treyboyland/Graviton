using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ReticleController : MonoBehaviour
{
    [SerializeField]
    float speed;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    [SerializeField]
    float fastMultiplier;

    [SerializeField]
    float slowMultiplier;

    float multiplier = 1.0f;

    public class Events
    {
        [Serializable]
        public class Flicker : UnityEvent { }
        [Serializable]
        public class StopFlickering : UnityEvent { }
    }

    [Serializable]
    public struct Restrictions
    {
        public Vector2 MinValues;
        public Vector2 MaxValues;
    }

    bool isFlickering = false;

    public bool IsFlickering
    {
        get
        {
            return isFlickering;
        }
        set
        {
            isFlickering = value;
        }
    }


    bool shouldMove = true;

    /// <summary>
    /// True if the reticle should move
    /// </summary>
    /// <value></value>
    public bool ShouldMove
    {
        get
        {
            return shouldMove;
        }
        set
        {
            shouldMove = value;
        }
    }

    [SerializeField]
    Restrictions restrictions;

    public Events.Flicker OnFlicker;

    public Events.StopFlickering OnStopFlickering;

    bool moveFast = false;
    bool moveSlow = false;

    Vector2 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        OnFlicker.AddListener(() => isFlickering = true);
        OnStopFlickering.AddListener(() => isFlickering = false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            SetMultiplier();
            DetermineMovement();
        }
    }

    public void HandleSpeedUpAction(bool value)
    {
        moveFast = value;
    }

    public void HandleSlowDownAction(bool value)
    {
        moveSlow = value;
    }

    public void HandleMoveAction(Vector2 move)
    {
        movementVector = move;
    }

    /// <summary>
    /// Modifies speed of reticle movement if one of the buttons is held down
    /// </summary>
    void SetMultiplier()
    {
        if ((!moveFast && !moveSlow) || (moveFast && moveSlow))
        {
            multiplier = 1;
        }
        else if (moveFast)
        {
            multiplier = 2f;
        }
        else
        {
            multiplier = 0.5f;
        }

    }

    void DetermineMovement()
    {
        bool up = movementVector.y > 0, down = movementVector.y < 0, 
            left = movementVector.x < 0, right = movementVector.x > 0;

        //Cancel contradictory directions
        if (up && down)
        {
            up = false;
            down = false;
        }
        if (left && right)
        {
            left = false;
            right = false;
        }

        Vector2 distance = new Vector2();

        if (left || right)
        {
            //If for some reason both are false and we are in here, don't move
            distance.x = (left ? -1 : (right ? 1 : 0)) * multiplier * speed * Time.deltaTime;
        }
        if (up || down)
        {
            //If for some reason both are false and we are in here, don't move
            distance.y = (down ? -1 : (up ? 1 : 0)) * multiplier * speed * Time.deltaTime;
        }

        //Bound positions
        Vector2 newPos = transform.position;
        newPos += distance;
        newPos.x = Mathf.Min(restrictions.MaxValues.x, Mathf.Max(restrictions.MinValues.x, newPos.x));
        newPos.y = Mathf.Min(restrictions.MaxValues.y, Mathf.Max(restrictions.MinValues.y, newPos.y));

        transform.position = newPos;
    }


}
