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
            speed = Mathf.Min(Mathf.Max(minSpeed, value), maxSpeed);
            BaseGameManager.Manager.OnPlayerSpeedUpdated.Invoke(speed);
        }
    }

    [SerializeField]
    float minSpeed;

    public float MinSpeed
    {
        get
        {
            return minSpeed;
        }
    }

    [SerializeField]
    float maxSpeed;

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
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

    [SerializeField]
    Rigidbody2D rigidbody;

    [SerializeField]
    Animator animator;

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
            BaseGameManager.Manager.OnScoreUpdated.Invoke(score);
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
        BaseGameManager.Manager.OnPointsReceived.AddListener((points) => Score += points);
        BaseGameManager.Manager.OnGrantPlayerInvincibility.AddListener(BecomeInvincible);
    }

    void Update()
    {
        MoveCharacter2();
    }

    void FixedUpdate()
    {
        ChangeTransform2();
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
    }

    void MoveCharacter2()
    {
        if (Input.GetButtonDown("Down"))
        {
            movingUp = false;
            movingDown = true;
            movingLeft = false;
            movingRight = false;
        }
        else if (Input.GetButtonDown("Up"))
        {
            movingUp = true;
            movingDown = false;
            movingLeft = false;
            movingRight = false;
        }
        else if (Input.GetButtonDown("Left"))
        {
            movingUp = false;
            movingDown = false;
            movingLeft = true;
            movingRight = false;
        }
        else if (Input.GetButtonDown("Right"))
        {
            movingUp = false;
            movingDown = false;
            movingLeft = false;
            movingRight = true;
        }
    }

    public bool IsInvincible()
    {
        return animator.GetBool("Invincible");
    }

    void BecomeInvincible(float secondsOfInvincibility)
    {
        Debug.LogWarning("Invincible!!!!");
        StopAllCoroutines();
        animator.SetBool("Invincible", false);
        StartCoroutine(WaitForInvincibility(secondsOfInvincibility));
    }

    IEnumerator WaitForInvincibility(float secondsOfInvincibility)
    {
        animator.SetBool("Invincible", true);
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        while(timer.Elapsed.TotalSeconds < secondsOfInvincibility)
        {
            yield return null;
        }

        animator.SetBool("Invincible", false);
    }

    void ChangeTransform2()
    {
        Vector2 newPos = gameObject.transform.position;

        float newX = newPos.x + (movingLeft ? -1.0f * speed : (movingRight ? speed : 0));
        newPos.x = newX;

        float newY = newPos.y + (movingDown ? -1.0f * speed : (movingUp ? speed : 0));
        newPos.y = newY;

        rigidbody.MovePosition(newPos);
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

        rigidbody.MovePosition(newPos);
    }
}
