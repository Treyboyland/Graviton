using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UndoRedoLevelEditorScript : MonoBehaviour
{
    Stack<ILevelEditorAction> undoActions = new Stack<ILevelEditorAction>();

    /// <summary>
    /// 
    /// </summary>
    Stack<ILevelEditorAction> redoActions = new Stack<ILevelEditorAction>();

    /// <summary>
    /// True if the user is allowed to undo actions
    bool allowUndo = true;

    /// <summary>
    /// True if the user is allowed to undo actions
    /// </summary>
    /// <value></value>
    public bool AllowUndo
    {
        get
        {
            return allowUndo;
        }
        set
        {
            allowUndo = value;
        }
    }

    public class Events
    {
        [Serializable]
        public class ActionDone : UnityEvent<ILevelEditorAction> { }
    }

    /// <summary>
    /// Should be invoked whenever the user completes an action in the editor
    /// </summary>
    public Events.ActionDone OnActionDone;

    // Start is called before the first frame update
    void Start()
    {
        OnActionDone.AddListener(action =>
        {
            redoActions.Clear();
            undoActions.Push(action);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (allowUndo)
        {
            HandleUndo();
        }
    }

    void HandleUndo()
    {
        if (Input.GetButtonDown("Undo"))
        {
            Undo();
        }
        else if (Input.GetButtonDown("Redo"))
        {
            Redo();
        }
    }



    /// <summary>
    /// Undo an action, and append it to the redo queue
    /// </summary>
    void Undo()
    {
        if (undoActions.Count != 0)
        {
            var action = undoActions.Pop();
            action.UndoAction();
            redoActions.Push(action);
        }
    }

    /// <summary>
    /// Redo an undone action
    /// </summary>
    void Redo()
    {
        if (redoActions.Count != 0)
        {
            var action = redoActions.Pop();
            action.RedoAction();
            undoActions.Push(action);
        }
    }
}
