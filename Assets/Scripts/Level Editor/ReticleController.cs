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

    [SerializeField]
    Restrictions restrictions;

    public Events.Flicker OnFlicker;

    public Events.StopFlickering OnStopFlickering;

    // Start is called before the first frame update
    void Start()
    {
        OnFlicker.AddListener(() => isFlickering = true);
        OnStopFlickering.AddListener(() => isFlickering = false);
    }

    // Update is called once per frame
    void Update()
    {
        DetermineMovement();
    }

    void DetermineMovement()
    {
        bool up = false, down = false, left = false, right = false;
        if (Input.GetButton("Up"))
        {
            up = true;
        }
        if (Input.GetButton("Down"))
        {
            down = true;
        }
        if (Input.GetButton("Left"))
        {
            left = true;
        }
        if (Input.GetButton("Right"))
        {
            right = true;
        }

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
            distance.x = (left ? -1 : right ? 1 : 0) * speed * Time.deltaTime;
        }
        if (up || down)
        {
            //If for some reason both are false and we are in here, don't move
            distance.y = (down ? -1 : up ? 1 : 0) * speed * Time.deltaTime;
        }

        //Bound positions
        Vector2 newPos = transform.position;
        newPos += distance;
        newPos.x = Mathf.Min(restrictions.MaxValues.x, Mathf.Max(restrictions.MinValues.x, newPos.x));
        newPos.y = Mathf.Min(restrictions.MaxValues.y, Mathf.Max(restrictions.MinValues.y, newPos.y));

        transform.position = newPos;
    }


}
