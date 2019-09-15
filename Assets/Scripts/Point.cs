using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    int maxPoints;

    int pointsAwarded;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float secondsToStayPerfect;

    [SerializeField]
    float secondsToFade;

    bool perfect = true;

    [SerializeField]
    new Collider2D collider;

    public Collider2D Collider
    {
        get
        {
            return collider;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Compare(other.gameObject.tag, "Player", true) == 0)
        {
            int points = GetPoints();
            BaseGameManager.Manager.OnPointsReceived.Invoke(points);
            BaseGameManager.Manager.OnPointTextAtPosition.Invoke(transform.position, spriteRenderer.color, points);

            if (perfect)
            {
                BaseGameManager.Manager.OnIncreasePlayerCombo.Invoke();
                BaseGameManager.Manager.OnPointsReceivedAtPosition.Invoke(transform.position, spriteRenderer.color);
            }
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {

            StartCoroutine(WaitThenFade());
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the alpha of this point
    /// </summary>
    /// <param name="a"></param>
    void SetAlpha(float a)
    {
        Color c = spriteRenderer.color;
        c.a = a;
        spriteRenderer.color = c;
    }

    /// <summary>
    /// Returns the amount of points this point is worth, based
    /// upon its condition
    /// </summary>
    /// <returns></returns>
    int GetPoints()
    {
        if (perfect)
        {
            return maxPoints;
        }

        float alpha = spriteRenderer.color.a;

        if (alpha <= .25f)
        {
            return maxPoints / 4;
        }
        else if (alpha <= .50f)
        {
            return maxPoints / 2;
        }
        else if (alpha <= .75f)
        {
            return 3 * maxPoints / 4;
        }

        return maxPoints;

    }



    IEnumerator WaitThenFade()
    {
        perfect = true;
        SetAlpha(1);
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        BaseGameManager.Manager.OnGamePaused.AddListener((paused) => TimerHelper.ToggleTimer(timer, paused));
        Coroutine co = StartCoroutine(TimerHelper.DisableIfPaused(timer));
        timer.Start();


        while (timer.Elapsed.TotalSeconds < secondsToStayPerfect)
        {
            yield return null;
        }

        timer.Reset();
        timer.Start();

        perfect = false;

        while (timer.Elapsed.TotalSeconds < secondsToFade)
        {
            SetAlpha(Mathf.Lerp(1, 0, (float)timer.Elapsed.TotalSeconds / secondsToFade));
            yield return null;
        }

        //TODO: What happens to this when the game ends?
        BaseGameManager.Manager.OnGamePaused.RemoveListener((paused) => TimerHelper.ToggleTimer(timer, paused));
        StopCoroutine(co);

        BaseGameManager.Manager.OnResetPlayerCombo.Invoke();
        gameObject.SetActive(false);
    }
}
