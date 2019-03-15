using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{

    System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnGamePaused.AddListener((paused) => TimerHelper.ToggleTimer(timer, paused));
        TimerHelper.DisableIfPaused(timer);
    }

    IEnumerator Countdown()
    {
        timer.Restart();

        while(timer.Elapsed.TotalSeconds < GameManager.Manager.GameTime)
        {
            yield return null;
        }
    }
}
