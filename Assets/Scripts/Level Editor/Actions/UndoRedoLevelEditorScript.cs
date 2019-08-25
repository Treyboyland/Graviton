using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedoLevelEditorScript : MonoBehaviour
{
    Stack<ILevelEditorAction> undoActions;

    Stack<ILevelEditorAction> redoActions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
