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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(string.Compare(other.gameObject.tag, "Player", true) == 0)
        {
            BaseGameManager.Manager.OnPointsReceived.Invoke(GetPoints());
            BaseGameManager.Manager.OnPointsReceivedAtPosition.Invoke(transform.position, spriteRenderer.color);
            if(perfect)
            {
                BaseGameManager.Manager.OnIncreasePlayerCombo.Invoke();
            }
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        if(gameObject.activeInHierarchy)
        {

            StartCoroutine(WaitThenFade());
        }
    }

    void SetAlpha(float a)
    {
        Color c = spriteRenderer.color;
        c.a = a;
        spriteRenderer.color = c;        
    }

    int GetPoints()
    {
        if(perfect)
        {
            return maxPoints;
        }

        float alpha = spriteRenderer.color.a;

        if(alpha <= .25f)
        {
            return maxPoints / 4;
        }
        else if(alpha <= .50f)
        {
            return maxPoints / 2;
        }
        else if(alpha <= .75f)
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
        timer.Start();

        while(timer.Elapsed.TotalSeconds < secondsToStayPerfect)
        {
            yield return null;
        }

        timer.Reset();
        timer.Start();

        perfect = false;

        while(timer.Elapsed.TotalSeconds < secondsToFade)
        {
            SetAlpha(Mathf.Lerp(1, 0,(float)timer.Elapsed.TotalSeconds / secondsToFade));
            yield return null;
        }

        BaseGameManager.Manager.OnResetPlayerCombo.Invoke();
        gameObject.SetActive(false);
    }
}
