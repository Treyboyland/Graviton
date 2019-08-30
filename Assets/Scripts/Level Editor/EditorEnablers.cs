using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorEnablers : MonoBehaviour
{
    [SerializeField]
    ReticleController reticle;

    [SerializeField]
    LevelEditorController levelEditor;

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            reticle.ShouldMove = false;
            levelEditor.ShouldHandleActions = false;
        }
    }

    private void OnDisable()
    {
        reticle.ShouldMove = true;
        levelEditor.ShouldHandleActions = true;
    }
}
