using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitEditorButton : MonoBehaviour
{
    [SerializeField]
    UndoController undoController;

    [SerializeField]
    Button button;

    [SerializeField]
    GameObject confirmation;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (undoController.IsUndoEmpty || undoController.WasLevelSaved)
            {
                SceneLoader.Loader.LoadLoadingScene();
            }
            else
            {
                confirmation.SetActive(true);
            }

        });
    }
}
