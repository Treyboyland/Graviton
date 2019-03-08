
using System.Collections;

/// <summary>
/// Contains helper methods for timers in the game
/// </summary>
public static class TimerHelper
{
    /// <summary>
    /// Starts the timer if not paused. Stops the timer if paused.
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="paused"></param>
    public static void ToggleTimer(System.Diagnostics.Stopwatch timer, bool paused)
    {
        if (paused)
        {
            timer.Stop();
        }
        else
        {
            timer.Start();
        }
    }

    public static IEnumerator DisableIfPaused(System.Diagnostics.Stopwatch timer)
    {
        while (true)
        {
            if (timer.IsRunning && GameManager.Manager.IsPaused)
            {
                timer.Stop();
            }
            yield return null;
        }
    }
}