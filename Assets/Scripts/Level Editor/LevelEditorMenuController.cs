using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelEditorMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    [SerializeField]
    Button backButton;


    [SerializeField]
    ReticleController reticle;

    [SerializeField]
    LevelEditorController levelEditor;

    [SerializeField]
    EditorTestSpawns spawnTester;

    [SerializeField]
    SymmetricWallPlacer symmetricPlacer;

    [SerializeField]
    UndoController undoController;

    [SerializeField]
    GameObject exitConfirmation;

    // Start is called before the first frame update
    void Start()
    {
        HideMenu();
    }

    public void ShowMenu()
    {
        reticle.ShouldMove = false;
        spawnTester.ShouldTest = false;
        levelEditor.ShouldHandleActions = false;
        symmetricPlacer.ShouldChange = false;
        undoController.AllowUndo = false;
        menu.SetActive(true);
        backButton.Select();
    }

    public void HideMenu()
    {
        StopAllCoroutines();
        menu.SetActive(false);
        exitConfirmation.SetActive(false);
        StartCoroutine(EnableMovement());
    }

    IEnumerator EnableMovement()
    {
        yield return null;
        reticle.ShouldMove = true;
        levelEditor.ShouldHandleActions = true;
        spawnTester.ShouldTest = true;
        symmetricPlacer.ShouldChange = true;
        undoController.AllowUndo = true;
    }

    /// <summary>
    /// Connect to pause event
    /// </summary>
    public void HandlePauseAction()
    {
        //NOTE: There was a reset call to input axes here before. Not sure if that exists under the new system
        Action action = menu.activeInHierarchy ? HideMenu : ShowMenu;
        action.Invoke();
    }
}
