using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public static class IdleTimerHelper
{
    public static bool ShouldRestartTimer = false;

    public static IEnumerator WaitForIdleTimeOut()
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        while (timer.Elapsed.TotalSeconds < GameConfigReader.ConfigReader.Configuration.IdleTimeout)
        {
            if (ShouldRestartTimer)
            {
                ShouldRestartTimer = false;
                //UnityEngine.Debug.LogWarning("Restarting timer");
                timer.Restart();
            }
            yield return null;
        }
    }
}
