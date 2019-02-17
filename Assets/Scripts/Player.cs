using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// How fast the player moves
    /// </summary>
    [SerializeField]
    float speed;

    /// <summary>
    /// How fast the player moves
    /// </summary>
    /// <value></value>
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

    bool movingLeft = false;
    bool movingRight = false;
    bool movingUp = false;
    bool movingDown = false;

    [SerializeField]
    Vector2 minPos;

    [SerializeField]
    Vector2 maxPos;

    int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    /// <summary>
    /// Speed the player has at the start of the game
    /// </summary>
    float startSpeed;

    /// <summary>
    /// Speed the player has at the start of the game
    /// </summary>
    /// <value></value>
    public float StartSpeed
    {
        get
        {
            return startSpeed;
        }
        set
        {
            startSpeed = value;
        }
    }

    void Start()
    {
        startSpeed = speed;
        BaseGameManager.Manager.OnPointsReceived.AddListener((points) => score += points);
    }

    void Update()
    {
        MoveCharacter();
    }

    /// <summary>
    /// Moves the player
    /// </summary>
    void MoveCharacter()
    {
        if (Input.GetButtonDown("Down"))
        {
            movingDown = true;
        }
        if (Input.GetButtonUp("Down"))
        {
            movingDown = false;
        }

        if (Input.GetButtonDown("Up"))
        {
            movingUp = true;
        }
        if (Input.GetButtonUp("Up"))
        {
            movingUp = false;
        }

        if (Input.GetButtonDown("Left"))
        {
            movingLeft = true;
        }
        if (Input.GetButtonUp("Left"))
        {
            movingLeft = false;
        }

        if (Input.GetButtonDown("Right"))
        {
            movingRight = true;
        }
        if (Input.GetButtonUp("Right"))
        {
            movingRight = false;
        }

        ChangeTransform();
    }

    void ChangeTransform()
    {
        //TODO: 4 D Input?
        Vector2 newPos = gameObject.transform.position;
        if (movingLeft && movingRight)
        {

        }
        else
        {
            float newX = newPos.x + (movingLeft ? -1.0f * speed : (movingRight ? speed : 0));
            if (newX < minPos.x)
            {
                newX = minPos.x;
            }
            else if (newX > maxPos.x)
            {
                newX = maxPos.x;
            }
            newPos.x = newX;
        }

        if (movingUp && movingDown)
        {

        }
        else
        {
            float newY = newPos.y + (movingDown ? -1.0f * speed : (movingUp ? speed : 0));
            if (newY < minPos.y)
            {
                newY = minPos.y;
            }
            else if (newY > maxPos.y)
            {
                newY = maxPos.y;
            }
            newPos.y = newY;
        }


        gameObject.transform.position = newPos;
    }
}
