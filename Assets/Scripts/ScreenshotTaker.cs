using System;
using System.IO;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    string screenshotDirectory = Application.dataPath + "/../Logs";

    static ScreenshotTaker _instance;

    void Awake()
    {
        if (_instance != null && this != _instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TakeScreenshot()
    {
        if (Directory.Exists(screenshotDirectory))
        {
            string fileName = Application.productName + "-" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff") + ".png";
            ScreenCapture.CaptureScreenshot(screenshotDirectory + "/" + fileName);
        }
    }
}
