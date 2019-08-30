using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        HideMenu();
    }

    public void ShowMenu()
    {
        reticle.ShouldMove = false;
        levelEditor.ShouldHandleActions = false;
        menu.SetActive(true);
        backButton.Select();
    }

    public void HideMenu()
    {
        StopAllCoroutines();
        menu.SetActive(false);
        StartCoroutine(EnableMovement());
    }

    IEnumerator EnableMovement()
    {
        yield return null;
        reticle.ShouldMove = true;
        levelEditor.ShouldHandleActions = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !menu.activeInHierarchy)
        {
            Input.ResetInputAxes();
            ShowMenu();
        }
        else if (Input.GetButtonDown("Pause") && menu.activeInHierarchy)
        {
            Input.ResetInputAxes();
            HideMenu();
        }
    }
}
