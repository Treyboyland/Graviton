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

    public int PointsAwarded
    {
        get
        {
            return pointsAwarded;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(string.Compare(other.gameObject.tag, "Player", true) == 0)
        {
            BaseGameManager.Manager.OnPointsReceived.Invoke(pointsAwarded);
            BaseGameManager.Manager.OnPointsReceivedAtPosition.Invoke(transform.position);
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


    IEnumerator WaitThenFade()
    {
        SetAlpha(1);
        pointsAwarded = maxPoints;
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        while(timer.Elapsed.TotalSeconds < secondsToStayPerfect)
        {
            yield return null;
        }

        timer.Reset();
        timer.Start();

        while(timer.Elapsed.TotalSeconds < secondsToFade)
        {
            pointsAwarded = (int)Mathf.Lerp(maxPoints, 0, (float)timer.Elapsed.TotalSeconds / secondsToFade);
            SetAlpha((1.0f) * pointsAwarded / maxPoints);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
