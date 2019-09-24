using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public class HighScoreExitTimer : MonoBehaviour
{
    [SerializeField]
    Button exitButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        if (GameConfigReader.ConfigReader.Configuration.IsArcadeCabinet)
        {
            StartCoroutine(WaitToExit());
        }
    }

    IEnumerator WaitToExit()
    {
        yield return StartCoroutine(IdleTimerHelper.WaitForIdleTimeOut());
        exitButton.onClick.Invoke();
    }
}
