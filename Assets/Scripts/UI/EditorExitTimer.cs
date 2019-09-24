using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorExitTimer : MonoBehaviour
{
    [SerializeField]
    UndoController undoController;

    [SerializeField]
    GameObject confirmation;

    [SerializeField]
    Button saveButton;

    [SerializeField]
    Button exitButton;

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

        confirmation.SetActive(false);

        if (undoController.WasLevelSaved || undoController.IsUndoEmpty)
        {
            exitButton.onClick.Invoke();
        }
        else
        {
            saveButton.onClick.Invoke();
            exitButton.onClick.Invoke();
        }
    }
}
