using System;
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
    Rigidbody2D playerbody;

    [SerializeField]
    Animator animator;

    [SerializeField]
    new Collider2D collider;

    public Collider2D Collider
    {
        get
        {
            return collider;
        }
    }

    int score = 0;

    /// <summary>
    /// Player's total score
    /// </summary>
    /// <value></value>
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

    /// <summary>
    /// Current combo level for the player
    /// </summary>
    int combo;

    /// <summary>
    /// Player's current combo level
    /// </summary>
    /// <value></value>
    public int Combo
    {
        get
        {
            return combo;
        }
        set
        {
            combo = value;
            //Debug.LogWarning("Player combo: " + combo);
            BaseGameManager.Manager.OnPlayerComboUpdated.Invoke(combo);
            MusicController.Instance.OnSetComboLevel.Invoke(combo);
        }
    }

    bool acceptingInput = true;

    bool isGameOver = false;

    /// <summary>
    /// True if the player is currently accepting input
    /// </summary>
    /// <value></value>
    public bool AcceptingInput
    {
        get
        {
            return acceptingInput;
        }
        set
        {
            acceptingInput = value;
        }
    }

    void Start()
    {
        startSpeed = speed;
        BaseGameManager.Manager.OnPointsReceived.AddListener((points) => Score += points);
        BaseGameManager.Manager.OnGrantPlayerInvincibility.AddListener(BecomeInvincible);
        BaseGameManager.Manager.OnResetPlayerCombo.AddListener(() => Combo = 0);
        BaseGameManager.Manager.OnIncreasePlayerCombo.AddListener(() => Combo++);
        BaseGameManager.Manager.OnGamePaused.AddListener((paused) =>
        {
            acceptingInput = !paused;
            animator.speed = paused ? 0 : 1;
        });
        BaseGameManager.Manager.OnGameOver.AddListener(() =>
        {
            isGameOver = true;
        });
    }

    void FixedUpdate()
    {
        if (acceptingInput && !isGameOver)
        {
            ChangeTransform();
        }
    }

    void SetBoolsLeft()
    {
        movingUp = false;
        movingDown = false;
        movingLeft = true;
        movingRight = false;
    }

    void SetBoolsRight()
    {
        movingUp = false;
        movingDown = false;
        movingLeft = false;
        movingRight = true;
    }

    void SetBoolsUp()
    {
        movingUp = true;
        movingDown = false;
        movingLeft = false;
        movingRight = false;
    }


    void SetBoolsDown()
    {
        movingUp = false;
        movingDown = true;
        movingLeft = false;
        movingRight = false;
    }

    /// <summary>
    /// Should be connected to event listener. Sets bools to move player 
    /// </summary>
    /// <param name="movementVector"></param>
    public void MoveCharacter(Vector2 movementVector)
    {
        if (!acceptingInput || isGameOver)
        {
            return;
        }
        movementVector = movementVector.IsolateGreater();

        if (movementVector != Vector2.zero)
        {
            Action movementMethod = null;
            bool leftRight = Mathf.Abs(movementVector.x) > 0;
            movementMethod = leftRight ? (movementVector.x < 0 ? SetBoolsLeft : SetBoolsRight) :
                (movementVector.y < 0 ? SetBoolsDown : SetBoolsUp);
        }
    }

    /// <summary>
    /// True if the player is invincible
    /// </summary>
    /// <returns></returns>
    public bool IsInvincible()
    {
        return animator.GetBool("Invincible");
    }

    /// <summary>
    /// Makes the player invincible
    /// </summary>
    /// <param name="secondsOfInvincibility">How long the player should stay invincible</param>
    void BecomeInvincible(float secondsOfInvincibility)
    {
        StopAllCoroutines();
        animator.SetBool("Invincible", false);
        StartCoroutine(WaitForInvincibility(secondsOfInvincibility));
    }

    /// <summary>
    /// Makes the player vulnerable at the end of the givein time
    /// </summary>
    /// <param name="secondsOfInvincibility"></param>
    /// <returns></returns>
    IEnumerator WaitForInvincibility(float secondsOfInvincibility)
    {
        animator.SetBool("Invincible", true);

        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        BaseGameManager.Manager.OnGamePaused.AddListener((paused) => TimerHelper.ToggleTimer(timer, paused));
        Coroutine co = StartCoroutine(TimerHelper.DisableIfPaused(timer));
        timer.Start();
        while (timer.Elapsed.TotalSeconds < secondsOfInvincibility)
        {
            yield return null;
        }
        BaseGameManager.Manager.OnGamePaused.RemoveListener((paused) => TimerHelper.ToggleTimer(timer, paused));
        StopCoroutine(co);

        animator.SetBool("Invincible", false);
    }

    /// <summary>
    /// Moves the player based upon the movement booleans
    /// </summary>
    void ChangeTransform()
    {
        Vector2 newPos = gameObject.transform.position;

        float newX = newPos.x + (movingLeft ? -1.0f * speed : (movingRight ? speed : 0));
        newPos.x = newX;

        float newY = newPos.y + (movingDown ? -1.0f * speed : (movingUp ? speed : 0));
        newPos.y = newY;

        playerbody.MovePosition(newPos);
    }
}
