using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{

    public class Events
    {
        [Serializable]
        public class NewTimeString : UnityEvent<string> { }
    }

    public Events.NewTimeString OnNewTimeString;

    System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnGamePaused.AddListener((paused) => TimerHelper.ToggleTimer(timer, paused));
        StartCoroutine(TimerHelper.DisableIfPaused(timer));
        StartCoroutine(Countdown());
    }

    public string GetTimeRemaining()
    {
        float secondsRemaining = (float)(GameManager.Manager.GameTime - timer.Elapsed.TotalSeconds);
        long ticks = (long)(secondsRemaining * TimeSpan.TicksPerSecond);
        TimeSpan span = new TimeSpan(ticks);

        if (secondsRemaining > 60)
        {
            //Do minutes and seconds    
            return span.ToString("mm\\:ss");
        }
        else
        {
            //Do seconds and tenths
            return span.ToString("ss\\.ff");
        }
    }

    IEnumerator Countdown()
    {
        
        timer.Restart();
        GetTimeRemaining();

        while(timer.Elapsed.TotalSeconds < GameManager.Manager.GameTime)
        {
            OnNewTimeString.Invoke(GetTimeRemaining());
            yield return null;
        }

        OnNewTimeString.Invoke("Time!");
        GameManager.Manager.OnGameOver.Invoke();

    }
}
