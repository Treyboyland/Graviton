using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField]
    float secondsToStayActive;

    private void OnEnable()
    {
        StartCoroutine(WaitThenDisable());
    }

    IEnumerator WaitThenDisable()
    {
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        GameManager.Manager.OnGamePaused.AddListener((paused) => TimerHelper.ToggleTimer(timer, paused));

        Coroutine coroutine = StartCoroutine(TimerHelper.DisableIfPaused(timer));
        timer.Start();

        while (timer.Elapsed.TotalSeconds < secondsToStayActive)
        {
            yield return null;
        }

        StopCoroutine(coroutine);

        gameObject.SetActive(false);
    }
}
