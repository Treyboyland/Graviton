using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public static class IdleTimerHelper
{
    /// <summary>
    /// True if any button was held down
    /// </summary>
    /// <returns></returns>
    static bool AnyButtonDown()
    {
        //This is disgusting...but there doesn't appear to be a better way to detect game input from controller
        return 
            //Input.GetAxis("Horizontal") != 0 ||
            //Input.GetAxis("Vertical") != 0 ||
            Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0 ||
            Input.GetAxis("Mouse ScrollWheel") != 0 ||
            Input.GetAxis("LeftRightJoy") != 0 ||
            Input.GetAxis("UpDownJoy") != 0 ||
            Input.GetButton("Submit") ||
            Input.GetButton("Cancel") ||
            Input.GetButton("Up") ||
            Input.GetButton("Down") ||
            Input.GetButton("Left") ||
            Input.GetButton("Right") ||
            Input.GetButton("Pause") ||
            Input.GetButton("SpeedUp") ||
            Input.GetButton("SlowDown") ||
            Input.GetButton("TestSpawns") ||
            Input.GetButton("ChangeSymmetry") ||
            Input.GetButton("Delete") ||
            Input.GetButton("Undo") ||
            Input.GetButton("Redo");
    }

    public static IEnumerator WaitForIdleTimeOut()
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        while (timer.Elapsed.TotalSeconds < GameConfigReader.ConfigReader.Configuration.IdleTimeout)
        {
            if (Input.anyKey || AnyButtonDown())
            {
                UnityEngine.Debug.LogWarning("Restarting timer");
                timer.Restart();
            }
            yield return null;
        }
    }
}
